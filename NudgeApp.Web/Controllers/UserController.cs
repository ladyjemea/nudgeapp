﻿namespace NudgeAppDataManagement.Web.Controllers
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
        private readonly IUserService UserLogic;
        private readonly AppSettings AppSettings;

        public UserController(IUserService userLogic, IOptions<AppSettings> appSettings)
        {
            this.UserLogic = userLogic;
            this.AppSettings = appSettings.Value;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string password, string name, string email, string address, TransportationType travelType)
        {

            if (this.UserLogic.CreateUser(password, name, email, address, travelType))
            {
                return this.Ok();
            }

            return this.BadRequest("User already exists!");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Authenticate(string email, string password)
        {
            var user = this.UserLogic.CheckPassword(email, password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = this.GenerateToken(user.Id);

            return Ok(new
            {
                Id = user.Id,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GoogleSignIn([FromBody] GoogleUser googleUser)
        {
            var id = this.UserLogic.VerifyGoogle(googleUser.Id, googleUser.TokenId);

            if (id != Guid.Empty)
            {
                var tokenString = GenerateToken(id);

                return this.Ok(new
                {
                    Id = id,
                    Username = googleUser.Email,
                    Token = tokenString
                });
            }

            return this.Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CheckToken()
        {
            return this.Ok();
        }

        private string GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public class GoogleUser
        {
            public string Id { get; set; }
            public string TokenId { get; set; }
            public string Email { get; set; }
        }
    }
}
