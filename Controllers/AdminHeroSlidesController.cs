using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;
using DroWashWebsite.Services;

namespace DroWashWebsite.Controllers
{
    public class AdminHeroSlidesController : AdminBaseController
    {
        private readonly ImageUploadService _images;
        private const string Folder = "hero-slideshow";

        public AdminHeroSlidesController(ApplicationDbContext db, ImageUploadService images) : base(db)
        {
            _images = images;
        }

        public async Task<IActionResult> Index()
        {
            var slides = await Db.HeroSlides.OrderBy(s => s.SortOrder).ToListAsync();
            return View(slides);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile imageFile, int sortOrder)
        {
            var (isValid, error) = _images.Validate(imageFile);
            if (!isValid)
            {
                ModelState.AddModelError("imageFile", error!);
                return View();
            }

            var url = await _images.SaveAsync(imageFile, Folder);

            Db.HeroSlides.Add(new HeroSlide { ImageUrl = url, SortOrder = sortOrder });
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Slide was added.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var slide = await Db.HeroSlides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? imageFile, int sortOrder)
        {
            var slide = await Db.HeroSlides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }

            // Only touch the image if a new file was actually chosen.
            if (imageFile != null && imageFile.Length > 0)
            {
                var (isValid, error) = _images.Validate(imageFile);
                if (!isValid)
                {
                    ModelState.AddModelError("imageFile", error!);
                    return View(slide);
                }

                var oldUrl = slide.ImageUrl;
                var newUrl = await _images.SaveAsync(imageFile, Folder);

                slide.ImageUrl = newUrl;

                // Delete the old physical file now that the new one is saved
                // and the DB row is about to be updated - no orphaned files left behind.
                _images.Delete(oldUrl);
            }

            slide.SortOrder = sortOrder;
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Slide was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var slide = await Db.HeroSlides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }

            _images.Delete(slide.ImageUrl);

            Db.HeroSlides.Remove(slide);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Slide was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}