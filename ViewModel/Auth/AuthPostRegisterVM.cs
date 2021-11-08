using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.ViewModel.Auth
{
    public class AuthPostRegisterVM
    {   
        [Required]
        [MaxLength(9)]
        public string UserName { get; set; }
        
        [Required]
        [MaxLength(9)]
        public string Password { get; set; }
    }
}
