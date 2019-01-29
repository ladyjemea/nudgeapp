namespace NudgeAppDataManagement.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataManagement.UserControl;

    [Route("Api/User")]
    public class UserController : Controller
    {
        private IUserLogic UserLogic;

        public UserController(IUserLogic userLogic)
        {
            this.UserLogic = userLogic;
        }

        [HttpGet]
        [Route("createUser")]
        public IActionResult Create(string username, string password)
        {
            this.UserLogic.CreateUser(username, password);

            return this.Ok();
        }
    }
}
