using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class AdminFeatureListsController : AdminBaseController
    {
        public AdminFeatureListsController(ApplicationDbContext db) : base(db)
        {
        }

        private static List<SelectListItem> CategoryOptions() => new()
        {
            new SelectListItem("Deionized Water Features", FeatureListCategory.Deionized.ToString()),
            new SelectListItem("Streak-Free Features", FeatureListCategory.StreakFree.ToString())
        };

        public async Task<IActionResult> Index()
        {
            var items = await Db.FeatureListItems
                .OrderBy(f => f.Category)
                .ThenBy(f => f.SortOrder)
                .ToListAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryOptions = CategoryOptions();
            return View(new FeatureListItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatureListItem model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryOptions = CategoryOptions();
                return View(model);
            }

            Db.FeatureListItems.Add(model);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Feature \"{model.Title}\" was created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await Db.FeatureListItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.CategoryOptions = CategoryOptions();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FeatureListItem model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryOptions = CategoryOptions();
                return View(model);
            }

            var existing = await Db.FeatureListItems.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Category = model.Category;
            existing.Number = model.Number;
            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.SortOrder = model.SortOrder;

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Feature \"{model.Title}\" was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await Db.FeatureListItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            Db.FeatureListItems.Remove(item);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Feature \"{item.Title}\" was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}