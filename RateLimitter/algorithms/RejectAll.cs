using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter.algorithms
{
    internal class RejectAll : IRateLimiter
    {
        public bool AllowRequest()
        {
            return false;
        }
    }
}
