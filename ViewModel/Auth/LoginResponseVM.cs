using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Auth
{
    public class LoginResponseVM
    {
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
