namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataAnalysis.Implementation;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Linq;

    [Route("Api/PushNotification")]
    public class PushNotificationController : Controller
    {
        private readonly IPushNotificationLogic PushNotificationLogic;
        private readonly IPushNotificationService PushNotificationService;

        public PushNotificationController(IPushNotificationLogic pushNotificationLogic, IPushNotificationService pushNotificationService)
        {
            this.PushNotificationService = pushNotificationService;
            this.PushNotificationLogic = pushNotificationLogic;
        }

        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe([FromBody] PushSubscription pushSubscription)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            this.PushNotificationLogic.SetSubscription(userId, pushSubscription);

            return this.Ok();
        }

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            this.PushNotificationService.PushAll();

            return this.Ok();
        }
    }
}
