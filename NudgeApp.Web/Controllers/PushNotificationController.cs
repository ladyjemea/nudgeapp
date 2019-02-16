namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataAnalysis.Implementation;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;

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
            var username = "lae";
            this.PushNotificationLogic.SetSubscription(username, pushSubscription);

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
