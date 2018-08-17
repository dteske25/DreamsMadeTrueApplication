using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DreamsMadeTrue.Accessors
{
    public class EmailAccessor : IEmailAccessor
    {
        private readonly SmtpClient _client;
        public EmailAccessor(IConfiguration configuration)
        {
            _client = new SmtpClient()
            {
                Host = configuration["SMTPSettings:Host"],
                EnableSsl = bool.Parse(configuration["SMTPSettings:EnableSsl"]),
                Port = int.Parse(configuration["SMTPSettings:Port"]),
                Credentials = new NetworkCredential(configuration["SMTPSettings:Username"], configuration["SMTPSettings:Password"])
            };

        }

        public async Task SendEmail(MailMessage emailMessage)
        {
            await _client.SendMailAsync(emailMessage);
        }
    }
}
