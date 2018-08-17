using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DreamsMadeTrue.Engines.Client.Dtos;
using DreamsMadeTrue.Engines.Client.Interfaces;
using DreamsMadeTrue.Web.Params;
using Microsoft.AspNetCore.Mvc;

namespace DreamsMadeTrue.Web.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserEngine _userEngine;
        private readonly IEmailEngine _emailEngine;
        public AuthController(IUserEngine userEngine, IEmailEngine emailEngine)
        {
            _userEngine = userEngine;
            _emailEngine = emailEngine;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserParams userParams)
        {
            var user = new UserDto
            {
                FirstName = userParams.FirstName,
                LastName = userParams.LastName,
                Email = userParams.Email,
                Username = userParams.Username,
            };
            var resultingUser = await _userEngine.CreateUser(user, userParams.Password);
            if (resultingUser != null)
            {
                var code = await _userEngine.GenerateEmailConfirmation(resultingUser);
                var url = $"{(HttpContext.Request.IsHttps ? "https://" : "http://")}{HttpContext.Request.Host}/register/confirm/{HtmlEncoder.Default.Encode(code)}";
                var text = $"Confirm your account here: {url} ";
                var html = $"<html><body><div>Click <a href=\"{url}\">here</a> to confirm your account</div></body></html>";
                await _emailEngine.SendEmail(new EmailDto
                {
                    ToAddresses = new List<string> { userParams.Email },
                    Subject = "Verify Your Account",
                    TextContent = text,
                    HtmlContent = html
                });
                //await _signInManager.SignInAsync(user, false);
                //return Json(new
                //{
                //    token = GenerateToken(user),
                //    userInfo = user
                //});
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserParams userParams)
        {
            var user = await _userEngine.FindUserByUsername(userParams.Username);
            var signInResult = await _userEngine.PasswordSignInUser(user, userParams.Password);
            if (signInResult.Succeeded)
            {
                return Json(new
                {
                    token = signInResult.Token,
                    userInfo = signInResult.UserInfo
                });
            }
            return Unauthorized();
        }
    }
}
