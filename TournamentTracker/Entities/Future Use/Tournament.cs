using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentTracker.Entities
{
    public class Tournament
    {
        public string TournamentName { get; set; }

        public decimal EntryFee { get; set; }

        public List<Team> EnteredTeams { get; set; } = new List<Team>();
        public List<int> PrizesIds { get; set; }

        [ForeignKey(nameof(PrizesIds))]

        public List<Prize> Prizes { get; set; } = new List<Prize>();  

        public List<List<Matchup>> Rounds { get; set; } = new List<List<Matchup>>();
    }
}
