using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TournamentTracker.Data;
using TournamentTracker.Models;

namespace TournamentTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TournamentTrackerDbContext _db;

        public IActionResult Index()
        {
            return View();
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
