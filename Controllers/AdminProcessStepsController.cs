using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class AdminProcessStepsController : AdminBaseController
    {
        public AdminProcessStepsController(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IActionResult> Index()
        {
            var steps = await Db.ProcessSteps.OrderBy(s => s.SortOrder).ToListAsync();
            return View(steps);
        }

        public IActionResult Create()
        {
            return View(new ProcessStep());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcessStep model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Db.ProcessSteps.Add(model);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Process step \"{model.Title}\" was created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var step = await Db.ProcessSteps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcessStep model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = await Db.ProcessSteps.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Number = model.Number;
            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.SortOrder = model.SortOrder;

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Process step \"{model.Title}\" was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var step = await Db.ProcessSteps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            Db.ProcessSteps.Remove(step);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Process step \"{step.Title}\" was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}