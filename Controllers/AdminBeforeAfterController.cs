using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;
using DroWashWebsite.Services;

namespace DroWashWebsite.Controllers
{
    public class AdminBeforeAfterController : AdminBaseController
    {
        private readonly ImageUploadService _images;
        private const string Folder = "before-after";

        public AdminBeforeAfterController(ApplicationDbContext db, ImageUploadService images) : base(db)
        {
            _images = images;
        }

        public async Task<IActionResult> Index()
        {
            var before = await Db.BeforeAfterImages.FirstOrDefaultAsync(b => b.Type == BeforeAfterType.Before);
            var after = await Db.BeforeAfterImages.FirstOrDefaultAsync(b => b.Type == BeforeAfterType.After);

            ViewBag.BeforeImage = before;
            ViewBag.AfterImage = after;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(BeforeAfterType type, IFormFile imageFile)
        {
            var (isValid, error) = _images.Validate(imageFile);
            if (!isValid)
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction(nameof(Index));
            }

            var existing = await Db.BeforeAfterImages.FirstOrDefaultAsync(b => b.Type == type);
            var newUrl = await _images.SaveAsync(imageFile, Folder);

            if (existing == null)
            {
                Db.BeforeAfterImages.Add(new BeforeAfterImage { Type = type, ImageUrl = newUrl });
            }
            else
            {
                var oldUrl = existing.ImageUrl;
                existing.ImageUrl = newUrl;

                // Remove the old file now that the new one is saved and the
                // DB row is updated - no orphaned files left behind.
                _images.Delete(oldUrl);
            }

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"{type} image was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(BeforeAfterType type)
        {
            var existing = await Db.BeforeAfterImages.FirstOrDefaultAsync(b => b.Type == type);
            if (existing != null)
            {
                _images.Delete(existing.ImageUrl);
                Db.BeforeAfterImages.Remove(existing);
                await Db.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = $"{type} image was removed.";
            return RedirectToAction(nameof(Index));
        }
    }
}