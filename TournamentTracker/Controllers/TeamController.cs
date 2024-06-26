using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TournamentTracker.Data;
using TournamentTracker.Entities;
using TournamentTracker.ViewModels.Team;

namespace TournamentTracker.Controllers
{
    public class TeamController : Controller
    {
        TournamentTrackerDbContext db = new TournamentTrackerDbContext();
        static List<Person> selectedTeamMembers = new List<Person>();
        [HttpGet]
        public IActionResult Index()
        {
            TeamVM model = new TeamVM();
            model.TeamMembers = selectedTeamMembers;
            var availablePlayers = db.People.ToList();
            model.AvailablePlayers = availablePlayers.Except(model.TeamMembers).ToList();
            return View(model);
        }
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost(TeamVM model)
        {
            Person newTeamMember = db.People.FirstOrDefault(x => x.Id == model.NewTeamMemberId);
            if (newTeamMember != null) 
            {
				selectedTeamMembers.Add(newTeamMember);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CreatePerson(TeamVM model)
        {
            Person person = model.NewMember;
            if (!string.IsNullOrEmpty(person.FirstName) &&
                !string.IsNullOrEmpty(person.LastName) &&
                !string.IsNullOrEmpty(person.EmailAddress))
            {
                db.Add(person);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
