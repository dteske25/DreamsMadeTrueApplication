using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Models;
using DreamsMadeTrue.Engines.Client.Dtos;
using DreamsMadeTrue.Engines.Client.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DreamsMadeTrue.Engines
{
    public class UserEngine : IUserEngine
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _tokenSigningKey;
        public UserEngine(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenSigningKey = configuration["TokenSigningKey"];
        }

        public async Task<UserDto> CreateUser(UserDto userInfo, string password)
        {
            var result = await _userManager.CreateAsync(userInfo.ToBaseObj(), password);
            if (result.Succeeded)
            {
                return await FindUserByEmail(userInfo.Email);
            }
            return null;
        }

        public async Task<UserDto> FindUserByEmail(string email)
        {
            var user = await GetApplicationUserByEmail(email);
            return user.ToDto();
        }

        public async Task<UserDto> FindUserById(string id)
        {
            var user = await GetApplicationUserById(id);
            return user.ToDto();
        }

        public async Task<UserDto> FindUserByUsername(string username)
        {
            var user = await GetApplicationUserByUsername(username);
            return user.ToDto();
        }

        public async Task<string> GenerateEmailConfirmation(UserDto userInfo)
        {
            var user = await GetApplicationUserById(userInfo.Id);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<SignInResultDto> ConfirmEmail(string userId, string code)
        {
            var user = await GetApplicationUserById(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new SignInResultDto
                {
                    Succeeded = true,
                    Token = GenerateJWT(user),
                    UserInfo = user.ToDto()
                };
            }
            return new SignInResultDto { Succeeded = false };
        }

        public async Task<SignInResultDto> PasswordSignInUser(UserDto userInfo, string password)
        {
            var user = await GetApplicationUserById(userInfo.Id);
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (result.Succeeded)
            {
                return new SignInResultDto
                {
                    Succeeded = true,
                    Token = GenerateJWT(user),
                    UserInfo = user.ToDto()
                };
            }
            return new SignInResultDto { Succeeded = false };
        }

        public async Task<SignInResultDto> SignInUser(UserDto userInfo)
        {
            var user = await GetApplicationUserById(userInfo.Id);
            await _signInManager.SignInAsync(user, false);
            return new SignInResultDto
            {
                Succeeded = true,
                Token = GenerateJWT(user),
                UserInfo = user.ToDto()
            };
        }

        private Task<ApplicationUser> GetApplicationUserById(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        private Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        private Task<ApplicationUser> GetApplicationUserByUsername(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        private string GenerateJWT(ApplicationUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(CustomClaimTypes.UserId, user.Id),
                    new Claim(CustomClaimTypes.Username, user.UserName),
                    new Claim(CustomClaimTypes.Email, user.Email),
                    new Claim(CustomClaimTypes.FirstName, user.FirstName),
                    new Claim(CustomClaimTypes.LastName, user.LastName),
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "dreams-made-true.org",
                audience: "dreams-made-true.org",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
