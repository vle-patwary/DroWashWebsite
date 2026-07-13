using Microsoft.AspNetCore.Mvc;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class QuoteController : Controller
    {
        private readonly ILogger<QuoteController> _logger;
        private readonly ApplicationDbContext _db;

        public QuoteController(ILogger<QuoteController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // POST /Quote/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(QuoteRequest model)
        {
            // Honeypot check: bots tend to fill every field, including ones
            // hidden from real users via CSS. If it's populated, pretend success
            // and drop the submission instead of telling the bot it was caught.
            if (!string.IsNullOrWhiteSpace(model.Website))
            {
                _logger.LogWarning("Quote submission blocked by honeypot from {Email}", model.Email);
                return Json(new { success = true });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kvp => kvp.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { success = false, errors });
            }

            try
            {
                // Reset server-controlled fields regardless of what the client sent.
                model.Id = 0;
                model.SubmittedAtUtc = DateTime.UtcNow;
                model.IsHandled = false;

                _db.QuoteRequests.Add(model);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Quote request saved for {Email}", model.Email);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save quote request for {Email}", model.Email);
                return Json(new
                {
                    success = false,
                    errors = new Dictionary<string, string[]>
                    {
                        ["General"] = new[] { "Something went wrong on our end. Please try again or call us directly." }
                    }
                });
            }
        }
    }
}