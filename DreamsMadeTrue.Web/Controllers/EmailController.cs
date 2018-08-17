using System.Collections.Generic;
using System.Threading.Tasks;
using DreamsMadeTrue.Engines.Client.Dtos;
using DreamsMadeTrue.Engines.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DreamsMadeTrue.Web.Controllers
{
    [Route("api/email")]
    public class EmailController : Controller
    {
        private readonly IEmailEngine _emailEngine;
        public EmailController(IEmailEngine emailEngine)
        {
            _emailEngine = emailEngine;
        }

        [HttpPost("new-account")]
        public async Task<IActionResult> SendNewAccountEmail([FromBody] string toEmail)
        {
            await _emailEngine.SendEmail(new EmailDto
            {
                ToAddresses = new List<string> { toEmail },
                Subject = "Verify Your Account",
                TextContent = "Please click <this link> to verify your account."
            });
            return Ok();
        }
    }
}
