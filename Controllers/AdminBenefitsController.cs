using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class AdminBenefitsController : AdminBaseController
    {
        public AdminBenefitsController(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IActionResult> Index()
        {
            var benefits = await Db.Benefits.OrderBy(b => b.SortOrder).ToListAsync();
            return View(benefits);
        }

        public IActionResult Create()
        {
            return View(new Benefit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Benefit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Db.Benefits.Add(model);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Benefit was created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var benefit = await Db.Benefits.FindAsync(id);
            if (benefit == null)
            {
                return NotFound();
            }

            return View(benefit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Benefit model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = await Db.Benefits.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Text = model.Text;
            existing.SortOrder = model.SortOrder;

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Benefit was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var benefit = await Db.Benefits.FindAsync(id);
            if (benefit == null)
            {
                return NotFound();
            }

            Db.Benefits.Remove(benefit);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Benefit was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}