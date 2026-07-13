using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class AdminServicesController : AdminBaseController
    {
        public AdminServicesController(ApplicationDbContext db) : base(db)
        {
        }

        // GET /AdminServices
        public async Task<IActionResult> Index()
        {
            var services = await Db.Services
                .OrderBy(s => s.SortOrder)
                .ToListAsync();

            return View(services);
        }

        // GET /AdminServices/Create
        public IActionResult Create()
        {
            return View(new Service());
        }

        // POST /AdminServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Db.Services.Add(model);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Service \"{model.Title}\" was created.";
            return RedirectToAction(nameof(Index));
        }

        // GET /AdminServices/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var service = await Db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST /AdminServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = await Db.Services.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Overwrite every field with the new values - old text is fully
            // replaced, matching how you want updates to behave.
            existing.Index = model.Index;
            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.IconSvgPath = model.IconSvgPath;
            existing.SortOrder = model.SortOrder;

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Service \"{model.Title}\" was updated.";
            return RedirectToAction(nameof(Index));
        }

        // POST /AdminServices/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await Db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            Db.Services.Remove(service);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Service \"{service.Title}\" was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}