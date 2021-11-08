using ChallengeDisney.ViewModel.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Character
{
    public class CharacterGetDetailsRequestVM
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Weight { get; set; }

        public string Story { get; set; }

        public List<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
    
    }
}
