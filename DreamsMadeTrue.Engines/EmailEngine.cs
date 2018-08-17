using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Interfaces;
using DreamsMadeTrue.Engines.Client.Dtos;
using DreamsMadeTrue.Engines.Client.Interfaces;

namespace DreamsMadeTrue.Engines
{
    public class EmailEngine : IEmailEngine
    {
        private readonly IEmailAccessor _emailAccessor;

        public EmailEngine(IEmailAccessor emailAccessor)
        {
            _emailAccessor = emailAccessor;
        }

        // This is an engine because I plan to add scheduled processes to this, and 
        // the accessor should only deal with sending the email itself
        public async Task SendEmail(EmailDto email)
        {
            var emailMessage = new MailMessage()
            {
                From = new MailAddress(email.FromAddress),
                Subject = email.Subject,
                Body = email.TextContent,
            };
            if (email?.ToAddresses?.Count() > 0)
            {
                emailMessage.To.Add(string.Join(',', email.ToAddresses));
            }
            if (email?.CcAddresses?.Count() > 0)
            {
                emailMessage.CC.Add(string.Join(',', email.CcAddresses));
            }
            if (email?.BccAddresses?.Count() > 0)
            {
                emailMessage.Bcc.Add(string.Join(',', email.BccAddresses));
            }
            if (!string.IsNullOrWhiteSpace(email.HtmlContent))
            {
                emailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(email.HtmlContent, new ContentType("text/plain")));
            }

            await _emailAccessor.SendEmail(emailMessage);
        }
    }
}
