using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Movie
{
    public class MoviePostRequestVM
    {
        public string Image { set; get; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; } 

        public int Qualification { get; set; }

        public List<int> CharactersId { get; set; }
    }
}
