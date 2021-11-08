using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Entidades
{
    public class Movie
    {
        public int Id { get; set; }

        public string Image { set; get; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }  

        public int  Qualification { get; set; }

        //Relaciones

        public ICollection<Character> Characters { get; set; }

        public Genre Genres { get; set; }
    }
}
