using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class AdminAdditionalServicesController : AdminBaseController
    {
        public AdminAdditionalServicesController(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IActionResult> Index()
        {
            var items = await Db.AdditionalServices.OrderBy(a => a.SortOrder).ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View(new AdditionalService());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdditionalService model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Db.AdditionalServices.Add(model);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"\"{model.Name}\" was added.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await Db.AdditionalServices.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdditionalService model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = await Db.AdditionalServices.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = model.Name;
            existing.SortOrder = model.SortOrder;

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"\"{model.Name}\" was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await Db.AdditionalServices.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            Db.AdditionalServices.Remove(item);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"\"{item.Name}\" was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}