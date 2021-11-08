using ChallengeDisney.Contexto;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Repositorio
{
    public class GenreRepository : BaseRepository<Genre, DisneyContext>, IGenreRepository
    {
        public GenreRepository(DisneyContext dbContext) : base(dbContext)
        {
          
        }

        public Genre GetGenre(int id)
        {
            return DbSet.Include(x => x.Movies).FirstOrDefault(y => y.Id == id);
        }

        public List<Genre> GetGenres()
        {
            return DbSet.Include(x => x.Movies).ToList();
        }
    }
}
