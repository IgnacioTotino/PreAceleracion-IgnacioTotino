﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeDisney.Entidades;

namespace ChallengeDisney.ViewModel.Genre
{
    public class GenrePutRequestVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}
