namespace TournamentTracker.ViewModels.Team
{
    using TournamentTracker.Entities;
    public class TeamVM
    {
        public string TeamName { get; set; }
        public List<Person> AllTeamMembers { get; set; } = new List<Person>();
        public List<Person> NewTeamMembers { get; set; } = new List<Person>();
        public int Id { get; set; }
        public int NewTeamMemberId { get; set; }
        public Person NewMember { get; set; }
        public Team selectedTeam { get; set; }
    }
}
