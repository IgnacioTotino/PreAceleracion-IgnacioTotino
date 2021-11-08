﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Movie
{
    public class MoviePutRequestVM
    {
        public int Id { get; set; }

        public string Image { set; get; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public int Qualification { get; set; }
    }
}
