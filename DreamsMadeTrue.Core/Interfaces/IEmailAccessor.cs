using System.Net.Mail;
using System.Threading.Tasks;

namespace DreamsMadeTrue.Core.Interfaces
{
    public interface IEmailAccessor
    {
        Task SendEmail(MailMessage emailMessage);
    }
}
