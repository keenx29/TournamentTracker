using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TournamentTracker.Entities
{
    public class Prize
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Place Number")]   
        public int PlaceNumber { get; set; }

        [Required]
		[DisplayName("Place Name")]
		public string PlaceName { get; set; }

		[DisplayName("Prize Amount")]
		public decimal PrizeAmount { get; set; }

		[DisplayName("Prize Percentage")]
        [Range(0, 100)]
		public double PrizePercentage { get; set; }
    }
}
