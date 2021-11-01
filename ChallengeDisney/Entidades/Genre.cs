using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Entidades
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public String SeriesAssociated { get; set; }

        //Relaciones    

        public ICollection<Movie> Movies { get; set;}
    }
}
