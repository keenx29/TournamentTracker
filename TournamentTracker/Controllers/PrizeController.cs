using Microsoft.AspNetCore.Mvc;
using TournamentTracker.Data;
using TournamentTracker.Entities;
using TournamentTracker.ViewModels.Prize;

namespace TournamentTracker.Controllers
{
    public class PrizeController : Controller
    {
        TournamentTrackerDbContext db = new TournamentTrackerDbContext();

        public IActionResult Index()
        {
            PrizeVM viewModel = new PrizeVM();
            viewModel.Prizes = db.Prizes.ToList();
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Create(Prize model)
		{
            if (ModelState.IsValid)
            {
                db.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			return RedirectToAction("Create", model);
		}
	}
}
