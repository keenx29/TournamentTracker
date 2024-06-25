using Microsoft.AspNetCore.Mvc;

namespace TournamentTracker.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
