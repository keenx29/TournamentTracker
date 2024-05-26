using Microsoft.AspNetCore.Identity;

namespace TournamentTracker.Entities
{
    public class Team
    {
        public List<Person> TeamMembers { get; set; } = new List<Person>();

        public string TeamName { get; set; }
    }
}
