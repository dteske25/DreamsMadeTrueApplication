using System.Threading.Tasks;
using DreamsMadeTrue.Engines.Client.Dtos;

namespace DreamsMadeTrue.Engines.Client.Interfaces
{
    public interface IEmailEngine
    {
        Task SendEmail(EmailDto email);
    }
}
