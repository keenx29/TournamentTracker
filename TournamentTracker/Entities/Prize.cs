using System.ComponentModel.DataAnnotations;

namespace TournamentTracker.Entities
{
    public class Prize
    {
        [Key]
        public int Id { get; set; }
        public int PlaceNumber { get; set; }

        [Required]
        public string PlaceName { get; set; }

        public decimal PrizeAmount { get; set; }

        public double PrizePercentage { get; set; }
    }
}
