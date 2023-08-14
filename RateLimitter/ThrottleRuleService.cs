using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter
{
    internal class ThrottleRuleService
    {
        private readonly Dictionary<string, string> Rules = new()
        {
            { "USER_IDENTIFICATION_SERVICE", "TokenBucket" }
        };
        public ThrottleRuleService() { }

        public string GetRule(string ruleName)
        {
            if (Rules.ContainsKey(ruleName)) return Rules[ruleName];
            // if Rule is not configured, return "";
            return string.Empty;
        }

    }
}
