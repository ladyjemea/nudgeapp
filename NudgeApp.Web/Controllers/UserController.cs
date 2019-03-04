namespace NudgeAppDataManagement.Web.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using NudgeApp.Web.Helpers;

    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserLogic UserLogic;
        private readonly AppSettings AppSettings;

        public UserController(IUserLogic userLogic, IOptions<AppSettings> appSettings)
        {
            this.UserLogic = userLogic;
            this.AppSettings = appSettings.Value;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string username, string password, string name, string email, string address, TransportationType travelType)
        {

            if (this.UserLogic.CreateUser(username, password, name, email, address, travelType))
            {
                return this.Ok();
            }

            return this.BadRequest("User already exists!");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Authenticate(string username, string password)
        {
            var user = this.UserLogic.CheckPassword(username, password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                Token = tokenString
            });
        }

        [Authorize]
        [HttpGet]
        public ActionResult CheckToken()
        {
            return this.Ok();
        }
    }
}
