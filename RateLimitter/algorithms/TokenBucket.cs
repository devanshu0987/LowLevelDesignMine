using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter.algorithms
{
    internal class TokenBucket : IRateLimiter
    {
        private long maxBucketSize;
        private long refillRate;
        private double currentBucketSize;
        private long lastRefillTimeStamp;

        object lockObject = new();
        public TokenBucket(long maxBucketSize, long refillRate)
        {
            this.maxBucketSize = maxBucketSize;
            this.refillRate = refillRate;
            currentBucketSize = maxBucketSize;
            lastRefillTimeStamp = DateTime.UtcNow.Ticks;

        }
        public bool AllowRequest()
        {
            lock (lockObject)
            {
                refill();
                if (currentBucketSize >= 1)
                {
                    currentBucketSize -= 1;
                    return true;
                }
                return false;
            }
        }

        private void refill()
        {
            long now = DateTime.UtcNow.Ticks;
            double tokensToAdd = (now - lastRefillTimeStamp) * refillRate / 1e7;
            Console.WriteLine("before refilled : " + currentBucketSize + "  ");
            currentBucketSize = Math.Min(currentBucketSize + tokensToAdd, maxBucketSize);
            Console.WriteLine("After refilled : " + currentBucketSize);
            lastRefillTimeStamp = now;
        }
    }
}
