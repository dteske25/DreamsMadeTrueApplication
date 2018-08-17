using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DreamsMadeTrue.Accessors
{
    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var returnedUser = await base.CreateAsync(user);
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.UserId, user.Id),
                new Claim(CustomClaimTypes.Username, user.UserName),
                new Claim(CustomClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.FirstName, user.FirstName),
                new Claim(CustomClaimTypes.LastName, user.LastName),
            };

            returnedUser.AddIdentity(new ClaimsIdentity(claims));
            return returnedUser;
        }
    }
}
