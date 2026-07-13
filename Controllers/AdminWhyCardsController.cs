using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class AdminWhyCardsController : AdminBaseController
    {
        public AdminWhyCardsController(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IActionResult> Index()
        {
            var cards = await Db.WhyCards.OrderBy(c => c.SortOrder).ToListAsync();
            return View(cards);
        }

        public IActionResult Create()
        {
            return View(new WhyCard());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WhyCard model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Db.WhyCards.Add(model);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Why-card \"{model.Title}\" was created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var card = await Db.WhyCards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WhyCard model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = await Db.WhyCards.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.IconSvgPath = model.IconSvgPath;
            existing.SortOrder = model.SortOrder;

            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Why-card \"{model.Title}\" was updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var card = await Db.WhyCards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            Db.WhyCards.Remove(card);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Why-card \"{card.Title}\" was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}