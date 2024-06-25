using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TournamentTracker.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
		[Required]
		[DisplayName("Last Name")]
		public string LastName { get; set; }
		[Required]
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }
    }
}
