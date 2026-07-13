using Microsoft.AspNetCore.Mvc;
using DroWashWebsite.Data;

namespace DroWashWebsite.Controllers
{
    public class AdminController : AdminBaseController
    {
        public AdminController(ApplicationDbContext db) : base(db)
        {
        }

        public IActionResult Index()
        {
            ViewData["ServicesCount"] = Db.Services.Count();
            ViewData["QuotesCount"] = Db.QuoteRequests.Count();
            ViewData["UnhandledQuotesCount"] = Db.QuoteRequests.Count(q => !q.IsHandled);
            ViewData["HeroSlidesCount"] = Db.HeroSlides.Count();
            return View();
        }
    }
}