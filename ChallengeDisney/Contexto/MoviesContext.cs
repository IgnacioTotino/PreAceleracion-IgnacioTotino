using ChallengeDisney.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Contexto
{
    public class MoviesContext : DbContext
    {
        private const string schema = "MoviesData";

        public MoviesContext (DbContextOptions options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
            ModelBuilder.HasDefaultSchema(schema);

           ModelBuilder.Entity<Character>().HasData(
                new Character()
                {
                    Id = 1,
                    Image = "hola",
                    Name = "Luffy",
                    Age = 19,
                    Weight = 40,
                    History = "IdontKnow",
                    MoviesAssociated = "One Piece"
                });

            ModelBuilder.Entity<Character>().HasData(
                new Character()
                {
                    Id = 2,
                    Image = "Holis",
                    Name = "Zoro",
                    Age = 21,
                    Weight = 70,
                    History = "Wano",
                    MoviesAssociated = "One Piece"
                });

            ModelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 3,
                    Image = "holaa",
                    Title = "Back to Future",
                    CreationDate = "1985",
                    Qualification = 5,
                    CharacterAssociated = "Marty McFly"
                },
                new Movie()
                {
                    Id = 4,
                    Image = "holaaa",
                    Title = "One Piece Stampede",
                    CreationDate = "2019",
                    Qualification = 5,
                    CharacterAssociated = "Luffy"
                });

            ModelBuilder.Entity<Genre>().HasData(
                new Genre()
                {
                    Id = 5,
                    Name = "Anime",
                    Image = "holaaaa",
                    SeriesAssociated = "One Piece"
                });
        }

        public DbSet<Character> Charaters { get; set; } = null;
        public DbSet<Movie> Movies { get; set; } = null;
        public DbSet<Genre> Genres { get; set; } = null;
    }
}
