using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;

namespace DroWashWebsite.Controllers
{
    public class AdminQuotesController : AdminBaseController
    {
        public AdminQuotesController(ApplicationDbContext db) : base(db)
        {
        }

        // GET /AdminQuotes?filter=unhandled
        public async Task<IActionResult> Index(string? filter)
        {
            var query = Db.QuoteRequests.AsQueryable();

            if (filter == "unhandled")
            {
                query = query.Where(q => !q.IsHandled);
            }

            var quotes = await query
                .OrderByDescending(q => q.SubmittedAtUtc)
                .ToListAsync();

            ViewBag.Filter = filter;
            return View(quotes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var quote = await Db.QuoteRequests.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleHandled(int id)
        {
            var quote = await Db.QuoteRequests.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            quote.IsHandled = !quote.IsHandled;
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = quote.IsHandled
                ? $"Marked {quote.FullName}'s request as handled."
                : $"Reopened {quote.FullName}'s request.";

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var quote = await Db.QuoteRequests.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            Db.QuoteRequests.Remove(quote);
            await Db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Quote request was deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}