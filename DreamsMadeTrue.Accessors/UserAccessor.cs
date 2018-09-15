using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Enums;
using DreamsMadeTrue.Core.Interfaces;
using DreamsMadeTrue.Core.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace DreamsMadeTrue.Accessors
{
    public class UserAccessor : MongoAccessor<ApplicationUser>, IUserAccessor
    {
        public UserAccessor(MongoContext context) : base(context) { }

        public async Task<ApplicationUser> AddToRoleAsync(ApplicationUser user, UserTypes role)
        {
            user.Roles = user.Roles?.Append(role).Distinct() ?? new List<UserTypes> { role };
            await Update(user);
            return user;
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await Insert(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await Delete(u => u.Id == user.Id);
            return IdentityResult.Success;
        }

        public void Dispose() { }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await First(u => u.NormalizedEmail == normalizedEmail);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await First(u => u.Id == userId);

        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await First(u => u.NormalizedUserName == normalizedUserName);
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var userToken = new UserToken { Name = purpose, Value = Guid.NewGuid().ToString() };
            user.Tokens = user.Tokens?.Append(userToken) ?? new List<UserToken>() { userToken };
            await Update(user);
            return userToken.Value;
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public async Task<ApplicationUser> RemoveFromRoleAsync(ApplicationUser user, UserTypes role)
        {
            user.Roles = user.Roles.Where(r => r != role);
            await Update(user);
            return user;
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await Update(user);
            return IdentityResult.Success;
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var tokens = user.Tokens?.ToList() ?? new List<UserToken>();
            var matchingTokens = tokens.Where(t => t.Name == purpose && t.Value == token);
            if (matchingTokens.Any())
            {
                user.Tokens = tokens.Where(t => !matchingTokens.Contains(t));
                await Update(user);
                return true;
            }
            return false;

        }
    }
}
