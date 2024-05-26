using Microsoft.AspNetCore.Mvc;
using TournamentTracker.Data;
using TournamentTracker.Entities;

namespace TournamentTracker.Controllers
{
    public class PrizeController : Controller
    {
        private readonly TournamentTrackerDbContext _db;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Create(Prize model)
		{
			return View();
		}
	}
}
