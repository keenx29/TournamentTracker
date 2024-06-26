using Microsoft.AspNetCore.Mvc;
using TournamentTracker.Data;
using TournamentTracker.Entities;
using TournamentTracker.ViewModels.People;

namespace TournamentTracker.Controllers
{
    public class PersonController : Controller
    {
		TournamentTrackerDbContext db = new TournamentTrackerDbContext();
		[HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Create(PersonVM model)
		{
            if (ModelState.IsValid)
            {
				Person newPerson = new Person();
				newPerson.FirstName = model.FirstName;
				newPerson.LastName = model.LastName;
				newPerson.EmailAddress = model.EmailAddress;
				db.People.Add(newPerson);
				db.SaveChanges();
				return RedirectToAction("Index", "Team");
			}
			return View(model);
		}
	}
}
