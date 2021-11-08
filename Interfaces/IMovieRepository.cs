using ChallengeDisney.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Interfaces
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        Movie GetMovie(int id);

        List<Movie> GetMovies();
    }
}
