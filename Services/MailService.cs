using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeDisney.Entidades;
using ChallengeDisney.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChallengeDisney.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _sengridClient;

        public MailService (ISendGridClient context)
        {
            _sengridClient = context;
        }

        public async Task SendEmail(User user)
        {
            var message = new SendGridMessage()
            {
                From = new EmailAddress("ignaciotomas.t@gmail.com", "API Disney Test"),
                Subject = "Inicializacion de usuario",
                PlainTextContent = $"Se ha creado el usuario: {user.UserName} de manera exitosa."
            };

            message.AddTo(new EmailAddress(user.Email, "Test User"));

            await _sengridClient.SendEmailAsync(message);
        }
    }
}
