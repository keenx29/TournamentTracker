using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TournamentTracker.Data;
using TournamentTracker.Entities;
using TournamentTracker.ViewModels.Team;

namespace TournamentTracker.Controllers
{
    public class TeamController : Controller
    {
        readonly TournamentTrackerDbContext db;
       

        public TeamController(TournamentTrackerDbContext context)
        {
            db = context;
        }
        //private List<Person> GetSelectedTeamMembers()
        //{
        //    var selectedTeamMembersJson = HttpContext.Session.GetString("SelectedTeamMembers");
        //    return selectedTeamMembersJson == null ? new List<Person>() : JsonConvert.DeserializeObject<List<Person>>(selectedTeamMembersJson);
        //}
        //static List<Person> selectedTeamMembers = new List<Person>();
        [HttpGet]
        public IActionResult Index(TeamVM model)
        {
            //TeamVM model = new TeamVM();
            //model.TeamMembers = selectedTeamMembers;
            if (TempData.Peek("TeamMembers") != null) {
                model.TeamMembers = JsonSerializer.Deserialize<List<Person>>(TempData["TeamMembers"].ToString());
            }
            var availablePlayers = db.People.ToList();
            model.AvailablePlayers = availablePlayers.Except(model.TeamMembers).ToList();
            return View(model);
        }
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost(TeamVM model)
        {
            //if (TempData.Peek("selectedTeamMembers") != null)
            //{
            //    model.TeamMembers = TempData.Peek("selectedTeamMembers").ToList();
            //}
            
            Person newTeamMember = db.People.FirstOrDefault(x => x.Id == model.NewTeamMemberId);
            if (newTeamMember != null) 
            {
                //selectedTeamMembers.Add(newTeamMember);
                model.TeamMembers.Add(newTeamMember);
                TempData["TeamMembers"] = JsonSerializer.Serialize(model.TeamMembers);
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
                model.TeamMembers.Add(person);
                TempData["TeamMembers"] = JsonSerializer.Serialize(model.TeamMembers);
            }
            return RedirectToAction("Index");
        }
    }
}
