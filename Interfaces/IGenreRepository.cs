using ChallengeDisney.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Interfaces
{
    interface IGenreRepository : IBaseRepository<Genre>
    {
        Genre GetGenre(int id);

        List<Genre> GetGenres();
    }
}
