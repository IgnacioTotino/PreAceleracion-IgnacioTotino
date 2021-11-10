using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeDisney.Entidades;
using ChallengeDisney.ViewModel.Movie;

namespace ChallengeDisney.ViewModel.Genre
{
    public class GenreGetRequestVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public List<MovieGetRequestVM> MovieOrSerie { get; set; } = new List<MovieGetRequestVM>();
    }
}
