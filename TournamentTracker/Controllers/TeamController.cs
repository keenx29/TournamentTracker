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
        static List<Person> selectedTeamMembers = new List<Person>();

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
            //if (TempData.Peek("TeamMembers") != null) {
            //    model.TeamMembers = JsonSerializer.Deserialize<List<Person>>(TempData["TeamMembers"].ToString());
            //}
            if (selectedTeamMembers != null) {
                model.TeamMembers = selectedTeamMembers;
            }
            var availablePlayers = db.People.ToList();

            foreach (var person in model.TeamMembers)
            {
                availablePlayers.RemoveAll(x => x.Id == person.Id); 
                //Does not work with Except method since the comparison is between static and regular List
                //TODO: Create a comparer to pass into the Except method.
            }

            model.AvailablePlayers = availablePlayers;
            return View(model);
        }
        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost(int? NewTeamMemberId)
        {
            //if (TempData.Peek("selectedTeamMembers") != null)
            //{
            //    model.TeamMembers = TempData.Peek("selectedTeamMembers").ToList();
            //}
            if (NewTeamMemberId != null && NewTeamMemberId != 0)
            {
                Person newTeamMember = db.People.FirstOrDefault(x => x.Id == NewTeamMemberId);
                if (newTeamMember != null)
                {
                    selectedTeamMembers.Add(newTeamMember);
                    //model.TeamMembers.Add(newTeamMember);
                    //var availablePlayers = db.People.ToList();
                    //model.AvailablePlayers = availablePlayers.Except(model.TeamMembers).ToList();
                    //TempData["TeamMembers"] = JsonSerializer.Serialize(model.TeamMembers);
                }
            }
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public IActionResult CreatePerson(TeamVM model)
        //{
        //    Person person = model.NewMember;
        //    if (!string.IsNullOrEmpty(person.FirstName) &&
        //        !string.IsNullOrEmpty(person.LastName) &&
        //        !string.IsNullOrEmpty(person.EmailAddress))
        //    {
        //        db.Add(person);
        //        db.SaveChanges();
        //        model.TeamMembers.Add(person);
        //        //TempData["TeamMembers"] = JsonSerializer.Serialize(model.TeamMembers);
        //    }
        //    return RedirectToAction("Index", model);
        //}
        [HttpPost]
        public IActionResult Create(string teamName)
        {
            //TODO: Check for a team already existing with the same name.
            if (!string.IsNullOrEmpty(teamName))
            {
                if (selectedTeamMembers.Count > 0)
                {
                    Team newTeam = new Team();
                    newTeam.TeamName = teamName;
                    db.Add(newTeam);
                    db.SaveChanges();
                    foreach (Person teamMember in selectedTeamMembers)
                    {
                        db.Attach(teamMember);
                        TeamMember newMember = new TeamMember();
                        newMember.Team = newTeam;
                        newMember.Person = teamMember;
                        db.Add(newMember);
                    }
                    db.SaveChanges();
                    selectedTeamMembers.Clear();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(int? RemovedTeamMemberId)
        {
            
            if (RemovedTeamMemberId != null && RemovedTeamMemberId != 0)
            {
                Person removedTeamMember = db.People.FirstOrDefault(x => x.Id == RemovedTeamMemberId);
                if (removedTeamMember != null)
                {
                    selectedTeamMembers.RemoveAll(x => x.Id == removedTeamMember.Id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
