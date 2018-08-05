using System.Threading.Tasks;
using DreamsMadeTrue.Core.Enums;
using DreamsMadeTrue.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace DreamsMadeTrue.Core.Interfaces
{
    public interface IUserAccessor : IUserStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IMongoAccessor<ApplicationUser>
    {
        Task<ApplicationUser> AddToRoleAsync(ApplicationUser user, UserTypes role);
        Task<ApplicationUser> RemoveFromRoleAsync(ApplicationUser user, UserTypes role);
    }
}
