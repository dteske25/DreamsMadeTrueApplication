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
                var link = $"{Request.Scheme}://{Request.Host}/api/auth/confirmEmail?userId={resultingUser.Id}&code={HtmlEncoder.Default.Encode(code)}";
                var text = $"Confirm your account here: {link} ";
                await _emailEngine.SendEmail(new EmailDto
                {
                    ToAddresses = new List<string> { userParams.Email },
                    Subject = "Verify Your Account",
                    TextContent = text
                });
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var signInResult = await _userEngine.ConfirmEmail(userId, code);
            return Json(signInResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserParams userParams)
        {
            var user = await _userEngine.FindUserByUsername(userParams.Username);
            var signInResult = await _userEngine.PasswordSignInUser(user, userParams.Password);
            if (signInResult.Succeeded)
            {
                return Json(signInResult);
            }
            return Unauthorized();
        }
    }
}
