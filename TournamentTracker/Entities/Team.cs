﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TournamentTracker.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public List<Person> TeamMembers { get; set; } = new List<Person>();

		[Required]
		[DisplayName("Team Name")]
		public string TeamName { get; set; }
    }
}
