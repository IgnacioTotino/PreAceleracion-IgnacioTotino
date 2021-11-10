using ChallengeDisney.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(User user);
    }
}
