namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataAnalysis.Implementation;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly INotificationService PushNotificationLogic;

        public NotificationController(INotificationService pushNotificationLogic)
        {
            this.PushNotificationLogic = pushNotificationLogic;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Subscribe([FromBody] PushSubscription pushSubscription)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            this.PushNotificationLogic.SetSubscription(userId, pushSubscription);

            return this.Ok();
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IList<Notification>> GetAll()
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            var result = this.PushNotificationLogic.GetAllNotifications(userId);

            return this.Ok(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Set(Guid notificationId, NudgeResult nudgeResult)
        {
            this.PushNotificationLogic.SetNudgeResult(notificationId, nudgeResult);

            return this.Ok();
        }
    }
}
