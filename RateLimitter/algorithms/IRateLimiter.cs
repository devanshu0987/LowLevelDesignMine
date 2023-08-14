using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter.algorithms
{
    internal interface IRateLimiter
    {
        bool AllowRequest();
    }
}
