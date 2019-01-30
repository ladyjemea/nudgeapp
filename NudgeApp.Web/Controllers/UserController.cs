namespace NudgeAppDataManagement.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    [Route("Api/User")]
    public class UserController : Controller
    {
        private readonly IUserLogic UserLogic;

        public UserController(IUserLogic userLogic)
        {
            this.UserLogic = userLogic;
        }

        [HttpGet]
        [Route("createUser")]
        public IActionResult Create(string username, string password, string name, string email, string address)
        {

            if (this.UserLogic.CreateUser(username, password, name, email, address))
            {
                return this.Ok();
            }

            return this.BadRequest("User already exists!");
        }

        [HttpGet]
        [Route("checkPassword")]
        public IActionResult CheckPassword(string username, string password)
        {
            if (this.UserLogic.CheckPassword(username, password))
            {
                return this.Ok();
            }

            return this.Unauthorized();
        }

        [HttpGet]
        [Route("updatePreferences")]
        public IActionResult UpdatePreferences(string userName, TransportationType travelType)
        {
            this.UserLogic.UpdateUserPreferences(userName, travelType);

            return this.Ok();
        }
    }
}
