using ChallengeDisney.ViewModel.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Movie
{
    public class MovieGetRequest2VM
    {
        public string Name { get; set; }

        public int GenreId { get; set; }

        public string order { get; set; }
    }
}
