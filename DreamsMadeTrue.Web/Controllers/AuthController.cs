using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DreamsMadeTrue.Core.Models;
using DreamsMadeTrue.Web.Params;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DreamsMadeTrue.Web.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserParams userParams)
        {
            var user = new ApplicationUser
            {
                FirstName = userParams.FirstName,
                LastName = userParams.LastName,
                Email = userParams.Email,
                UserName = userParams.Username,
            };
            var result = await _userManager.CreateAsync(user, userParams.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Json(new
                {
                    token = GenerateToken(user),
                    userInfo = user
                });
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserParams userParams)
        {
            var user = await _userManager.FindByNameAsync(userParams.Username);
            var result = await _signInManager.PasswordSignInAsync(user, userParams.Password, false, false);
            if (result.Succeeded)
            {
                return Json(new
                {
                    token = GenerateToken(user),
                    userInfo = user
                });
            }
            return Unauthorized();
        }

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{

        //}

        //Login
        //Logout
        private string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(CustomClaimTypes.UserId, user.Id),
                    new Claim(CustomClaimTypes.Username, user.UserName),
                    new Claim(CustomClaimTypes.Email, user.Email),
                    new Claim(CustomClaimTypes.FirstName, user.FirstName),
                    new Claim(CustomClaimTypes.LastName, user.LastName),
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkeythatillinjectlater"));
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

    public class CustomClaimTypes
    {
        public const string UserId = "userId";
        public const string Username = "username";
        public const string Email = "email";
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
    }
}
