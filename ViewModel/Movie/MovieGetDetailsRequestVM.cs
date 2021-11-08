using ChallengeDisney.ViewModel.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Movie
{
    public class MovieGetDetailsRequestVM
    {
        public int Id { get; set; }

        public string Image { set; get; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; } //cambiar a DateTime

        public int Qualification { get; set; }

        public List<CharacterGetDetailsRequestVM> Characters { get; set; } = new List<CharacterGetDetailsRequestVM>(); //mal

    }
}
