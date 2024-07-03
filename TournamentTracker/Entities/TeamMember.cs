using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentTracker.Entities
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }

        public int PersonId { get; set; }
        
        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }
        

    }
}
