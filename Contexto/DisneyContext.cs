using ChallengeDisney.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Contexto
{
    public class DisneyContext : DbContext
    {
        private const string schema = "MoviesData";

        public DisneyContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
            ModelBuilder.HasDefaultSchema(schema);

          

            ModelBuilder.Entity<Character>().HasData(
                new Character()
                {
                    Id = 2,
                    Image = "Holis",
                    Name = "Zoro",
                    Age = 21,
                    Weight = 70,
                    Story = "Wano",
                },
                new Character()
                { 
                    Id = 4,
                    Image = "ghost",
                    Name = "Perona",
                    Age = 17,
                    Weight = 45,
                    Story ="Thriler Bark",
                    

                });

            ModelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 3,
                    Image = "holaa",
                    Title = "Back to Future",
                    CreationDate = DateTime.Parse("01/01/1999"),
                    Qualification = 5,                   
                },
                new Movie()
                {
                    Id = 4,
                    Image = "holaaa",
                    Title = "One Piece Stampede",
                    CreationDate = DateTime.Parse("01/01/2019"),
                    Qualification = 5,
                });

            ModelBuilder.Entity<Genre>().HasData(
                new Genre()
                {
                    Id = 5,
                    Name = "Anime",
                    Image = "holaaaa",   
                },
                new Genre()
                {
                    Id = 6,
                    Name ="Ciencia Ficcion",
                    Image="TestImage"
                });


        }

        public DbSet<Character> Charaters { get; set; } = null;
        public DbSet<Movie> Movies { get; set; } = null;
        public DbSet<Genre> Genres { get; set; } = null;
    }
}
