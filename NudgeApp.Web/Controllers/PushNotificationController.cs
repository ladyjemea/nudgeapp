namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    [Route("Api/PushNotification")]
    public class PushNotificationController : Controller
    {
        private readonly IPushNotificationLogic PushNotificationLogic;

        public PushNotificationController(IPushNotificationLogic pushNotificationLogic)
        {
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
    }
}
