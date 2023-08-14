using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter
{
    /// <summary>
    /// Ideally a service just specify that I want to use this rule to throttle my requests
    /// And RateLimiter service keeps track of all those rules and application of rules.
    /// And it should also not know about any of the specific fuctionality of services ideally.
    /// </summary>
    internal class UserIdentificationService
    {
        RateLimitterService RateLimitterService { get; set; }
        // We will use this to find the specific implementation.
        readonly string ThrottleRuleId = "USER_IDENTIFICATION_SERVICE";

        // Why are we initializing a new RateLimitter service here? Shuoldnt you be a singleton?
        // Will figure it out.
        public UserIdentificationService()
        {
            RateLimitterService = new RateLimitterService();
        }

        public string ServeRequest(string key)
        {
            bool status = RateLimitterService.AllowRequest(key, ThrottleRuleId);
            if (status)
            {
                return "Request Served " + key;
            }
            return "429 " + key;
        }
    }
}
