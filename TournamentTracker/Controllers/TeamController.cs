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
        public IActionResult IndexPost(TeamVM model)
        {
            //if (TempData.Peek("selectedTeamMembers") != null)
            //{
            //    model.TeamMembers = TempData.Peek("selectedTeamMembers").ToList();
            //}
            
            Person newTeamMember = db.People.FirstOrDefault(x => x.Id == model.NewTeamMemberId);
            if (newTeamMember != null) 
            {
                selectedTeamMembers.Add(newTeamMember);
                //model.TeamMembers.Add(newTeamMember);
                //var availablePlayers = db.People.ToList();
                //model.AvailablePlayers = availablePlayers.Except(model.TeamMembers).ToList();
                //TempData["TeamMembers"] = JsonSerializer.Serialize(model.TeamMembers);
            }
            return RedirectToAction("Index",model);
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
        public IActionResult Create(TeamVM model)
        {
            Team newTeam = new Team();
            if (!string.IsNullOrEmpty(model.TeamName))
            {
                if (selectedTeamMembers.Count > 0)
                {
                    newTeam.TeamName = model.TeamName;
                    foreach (Person teamMember in selectedTeamMembers)
                    {
                        newTeam.TeamMembers.Add(teamMember);
                    }
                    db.Add(newTeam);
                    db.SaveChanges();
                    return View();
                }
            }
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult Remove(int? Id)
        {
            //TODO: Id of the team member to be removed is not coming through.
            Person removedTeamMember = db.People.FirstOrDefault(x => x.Id == Id);
            if (Id != null)
            {
                if (Id != 0)
                {
                    if (removedTeamMember != null)
                    {
                        selectedTeamMembers.RemoveAll(x => x.Id == Id);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
