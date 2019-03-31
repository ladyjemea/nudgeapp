namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache DistributedCache;

        public PushNotificationController(IPushNotificationLogic pushNotificationLogic, IPushNotificationService pushNotificationService, IDistributedCache distributedCache)
        {
            this.PushNotificationService = pushNotificationService;
            this.PushNotificationLogic = pushNotificationLogic;
            this.DistributedCache = distributedCache;
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
        [AllowAnonymous]
        public IActionResult Test()
        {
            var cacheKey = "TheTime";
            var existingTime = this.DistributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(existingTime))
            {
                return this.Ok("Fetched from cache : " + existingTime);
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                this.DistributedCache.SetString(cacheKey, existingTime, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = new TimeSpan(2, 0, 0) });
                return this.Ok("Added to cache : " + existingTime);
            }
        }
    }
}
