using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter.algorithms
{
    internal class TokenBucket : IRateLimiter
    {
        int counter = 0;
        public TokenBucket() { }
        public bool AllowRequest()
        {
            Interlocked.Increment(ref counter);
            if (counter > 5)
            {
                return false;
            }
            return true;
        }
    }
}
