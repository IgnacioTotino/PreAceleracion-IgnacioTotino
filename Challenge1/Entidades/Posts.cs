using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Entidades
{
    public class Posts {

        public string title { get; set; }

        public DateTime date { get; set; }

        public string content { get; set; }

        public string userCommit { get; set; }

        public Comments idCommet { get; set; }

    }
}
