using Microsoft.EntityFrameworkCore;
using TournamentTracker.Entities;

namespace TournamentTracker.Data
{
    public class TournamentTrackerDbContext : DbContext
    {
        public DbSet<Prize> Prizes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-SH85319;Database=TournamentTrackerDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(new User()
        //    {
        //        Id = 1,
        //        Username = "plameni",
        //        Password = "pass"
        //    });
        //}
    }

}
