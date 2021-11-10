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
    public class MovieRepository : BaseRepository<Movie, DisneyContext>, IMovieRepository
    {
        public MovieRepository(DisneyContext dbContext) : base(dbContext)
        {

        }

        public Movie GetMovie(int id)
        {
            return DbSet.Include(x => x.Genres).Include(z => z.Characters).FirstOrDefault(y => y.Id == id);
        }

        public List<Movie> GetMovies()
        {
            return DbSet.Include(x => x.Genres).ToList();
        }
    }
}
