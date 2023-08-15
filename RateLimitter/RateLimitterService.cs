using RateLimitter.algorithms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter
{
    // It knows about rules and how to apply them.
    // string - RateLimitter mapping.
    // We get request which gives us key
    // Using key we find the specific rule that apply on it.
    // using that, we find the specific RateLimiter implementation that apply on it
    // Then we check if we have to throttle.

    // User Identification service will get multiple UserIds.
    // I need to maintain 1 RateLimitter instance for each one of them.
    // If I get a new request, I need to check if I already have an instance, if yes, use it.
    // Else, create it according to the rule.

    // Rule Id to Algo mapping.
    public class RateLimitterService
    {
        ConcurrentDictionary<string, IRateLimiter> RateLimitterMap;
        ThrottleRuleService throttleRuleService;

        public RateLimitterService()
        {
            RateLimitterMap = new ConcurrentDictionary<string, IRateLimiter>();
            throttleRuleService = new ThrottleRuleService();
        }

        public bool AllowRequest(string key, string ruleId)
        {
            if (!RateLimitterMap.ContainsKey(key))
            {
                // if it already contains the key, we dont need to intialize a new one.
                // Use Key-RuleId to get the specific RateLimiter instance.
                string ruleName = throttleRuleService.GetRule(ruleId);
                // fetch config for the specific key, i.e what specific values to pass into the functions.
                // Here we are just sending 5,10. It can change.
                switch (ruleName)
                {
                    case "TokenBucket":
                        RateLimitterMap.TryAdd(key, new TokenBucket(5, 10));
                        break;
                    default:
                        RateLimitterMap.TryAdd(key, new RejectAll());
                        break;
                }
            }

            RateLimitterMap.TryGetValue(key, out var rateLimiter);
            return rateLimiter.AllowRequest();
        }
    }
}
