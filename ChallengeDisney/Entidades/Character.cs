using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Entidades
{
    public class Character
    {
        public int Id { get; set; }
        
        public string Image { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }        //TODO: cambiar el tipo de edad a string y hacer una migration 

        public int Weight { get; set; }

        public string History { get; set; }

        public string MoviesAssociated { get; set; }

        public Movie Movies { get; set; }
    }
}
