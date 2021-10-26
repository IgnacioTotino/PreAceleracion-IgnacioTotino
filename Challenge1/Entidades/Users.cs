using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Entidades
{
    public class Users{

        public int idUser { get; set; }

        public string name { get; set; }  

        public string password { get; set; }

        public string email { get; set; }

        public int post { get; set; }

        public string comments { get; set; }

        public ICollection<Comments> comment { get; set; }
    }
}
