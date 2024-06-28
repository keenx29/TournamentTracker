namespace TournamentTracker.ViewModels.Team
{
    using TournamentTracker.Entities;
    public class TeamVM
    {
        public string TeamName { get; set; }
        public List<Person> AvailablePlayers { get; set; } = new List<Person>(); //Stores all people except the members of the team
        public List<Person> TeamMembers { get; set; } = new List<Person>(); //Stores all members of the team
        public int NewTeamMemberId { get; set; } //For adding a person to the team
        public Person NewMember { get; set; } //For creating a new person and adding it to the DB and the team
	}
}
