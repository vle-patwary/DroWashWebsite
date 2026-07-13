using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DroWashWebsite.Data;
using DroWashWebsite.Models;

namespace DroWashWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _db.Services.OrderBy(s => s.SortOrder).ToListAsync();
            var additionalServices = await _db.AdditionalServices.OrderBy(a => a.SortOrder).ToListAsync();
            var benefits = await _db.Benefits.OrderBy(b => b.SortOrder).ToListAsync();
            var processSteps = await _db.ProcessSteps.OrderBy(p => p.SortOrder).ToListAsync();
            var whyCards = await _db.WhyCards.OrderBy(w => w.SortOrder).ToListAsync();
            var heroSlides = await _db.HeroSlides.OrderBy(h => h.SortOrder).ToListAsync();

            var deionizedFeatures = await _db.FeatureListItems
                .Where(f => f.Category == FeatureListCategory.Deionized)
                .OrderBy(f => f.SortOrder)
                .ToListAsync();

            var streakFreeFeatures = await _db.FeatureListItems
                .Where(f => f.Category == FeatureListCategory.StreakFree)
                .OrderBy(f => f.SortOrder)
                .ToListAsync();

            var beforeImage = await _db.BeforeAfterImages.FirstOrDefaultAsync(b => b.Type == BeforeAfterType.Before);
            var afterImage = await _db.BeforeAfterImages.FirstOrDefaultAsync(b => b.Type == BeforeAfterType.After);

            var model = new HomeViewModel
            {
                BeforeImageUrl = beforeImage?.ImageUrl,
                AfterImageUrl = afterImage?.ImageUrl,
                HeroSlides = heroSlides.Select(h => h.ImageUrl).ToList(),

                Services = services.Select(s => new ServiceItem
                {
                    Index = s.Index,
                    Title = s.Title,
                    Description = s.Description,
                    IconSvgPath = s.IconSvgPath
                }).ToList(),

                AdditionalServices = additionalServices.Select(a => a.Name).ToList(),
                Benefits = benefits.Select(b => b.Text).ToList(),

                ProcessSteps = processSteps.Select(p => new ProcessStep
                {
                    Number = p.Number,
                    Title = p.Title,
                    Description = p.Description
                }).ToList(),

                WhyCards = whyCards.Select(w => new WhyCard
                {
                    Title = w.Title,
                    Description = w.Description,
                    IconSvgPath = w.IconSvgPath
                }).ToList(),

                DeionizedFeatures = deionizedFeatures.Select(f => new FeatureListItem
                {
                    Number = f.Number,
                    Title = f.Title,
                    Description = f.Description
                }).ToList(),

                StreakFreeFeatures = streakFreeFeatures.Select(f => new FeatureListItem
                {
                    Number = f.Number,
                    Title = f.Title,
                    Description = f.Description
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}