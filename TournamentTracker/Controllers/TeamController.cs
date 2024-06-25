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
        public IActionResult Index(TeamVM model)
        {
            model.AllTeamMembers = db.People.ToList(); 
            return View(model);
        }
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost(TeamVM model)
        {
            Person newMember = db.People.FirstOrDefault(x => x.Id == model.Id);
            if (newMember != null) {
                model.NewTeamMembers.Add(newMember);
            }
            
            return View(model);
        }
        [HttpPost]
        public IActionResult CreatePerson(TeamVM model)
        {
            Person person = model.NewMember;
            if (person.FirstName != "" && person.LastName != "" && person.EmailAddress != "")
            {
                db.Add(person);
                db.SaveChanges();
                model.NewTeamMembers.Add(person);
                return RedirectToAction("Index",model);
            }
            return RedirectToAction("Index",model);
        }
    }
}
