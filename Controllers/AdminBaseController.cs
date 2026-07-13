using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DroWashWebsite.Data;

namespace DroWashWebsite.Controllers
{
    [Authorize]
    public abstract class AdminBaseController : Controller
    {
        protected readonly ApplicationDbContext Db;

        protected AdminBaseController(ApplicationDbContext db)
        {
            Db = db;
        }
    }
}