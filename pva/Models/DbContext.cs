using Microsoft.EntityFrameworkCore;
using pva.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Station> Stations { get; set; }
    public DbSet<Value> Values { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Value>().HasData(
            new Value
            {
                ValueId = 1,
                Level = 41,
                Timestamp = DateTime.Now,
                StationId = 1,
            },

            new Value
            {
                ValueId = 2,
                Level = 65,
                Timestamp = DateTime.Now,
                StationId = 2,
            },

            new Value
            {
                ValueId = 3,
                Level = 27,
                Timestamp = DateTime.Now,
                StationId = 3,
            },

            new Value
            {
                ValueId = 4,
                Level = 73,
                Timestamp = DateTime.Now,
                StationId = 4,
            });

        modelBuilder.Entity<Station>().HasData(
            new Station
            {
                StationId = 1,
                Name = "Colorado River",
                FloodLevel = 90,
                DroughtLevel = 40,
                TimeOutinMinutes = 35,
            },

            new Station
            {
                StationId = 2,
                Name = "Mississippi",
                FloodLevel = 90,
                DroughtLevel = 40,
                TimeOutinMinutes = 35,
            },

            new Station
            {
                StationId = 3,
                Name = "Amargosa",
                FloodLevel = 70,
                DroughtLevel = 1,
                TimeOutinMinutes = 95,
            },

            new Station
            {
                StationId = 4,
                Name = "Gila",
                FloodLevel = 70,
                DroughtLevel = 1,
                TimeOutinMinutes = 95,
            },

            new Station
            {
                StationId = 5,
                Name = "Hudson",
                FloodLevel = 85,
                DroughtLevel = 35,
                TimeOutinMinutes = 65,
            },

            new Station
            {
                StationId = 6,
                Name = "Columbia",
                FloodLevel = 80,
                DroughtLevel = 30,
                TimeOutinMinutes = 45,
            },

            new Station
            {
                StationId = 7,
                Name = "Snake",
                FloodLevel = 75,
                DroughtLevel = 25,
                TimeOutinMinutes = 45,
            },

            new Station
            {
                StationId = 8,
                Name = "Arkansas",
                FloodLevel = 88,
                DroughtLevel = 38,
                TimeOutinMinutes = 75,
            },

            new Station
            {
                StationId = 9,
                Name = "Rio Grande",
                FloodLevel = 77,
                DroughtLevel = 27,
                TimeOutinMinutes = 25,
            },

            new Station
            {
                StationId = 10,
                Name = "Yukon",
                FloodLevel = 80,
                DroughtLevel = 30,
                TimeOutinMinutes = 65,
            });
    }
}
