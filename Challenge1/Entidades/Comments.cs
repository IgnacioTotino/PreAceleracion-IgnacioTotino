using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge1.Entidades
{
    public class Comments{

        public string idComment { get; set; }

        public DateTime date { get; set; }

        public string comment { get; set; }

        public string userCommet { get; set; }

        //relacion com comments

        public Users idUser { get; set; }

        //relaciion con post

        public ICollection<Posts> post { get; set; }
 
}
