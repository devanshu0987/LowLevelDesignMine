using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitter.algorithms
{
    /// <summary>
    /// If the number of requests served on configuration key 
    /// in the last time_window_sec seconds is more than number_of_requests 
    /// configured for it then discard, else the request goes through while we update the counter.
    /// </summary>
    internal class SlidingWindow : IRateLimiter
    {
        ConcurrentQueue<DateTime> Queue;
        long windowSizeInSeconds;
        long bucketSize;
        object lockObject = new();

        public SlidingWindow(long bucketSize, long windowSizeInSeconds)
        {
            Queue = new ConcurrentQueue<DateTime>();
            this.bucketSize = bucketSize;
            this.windowSizeInSeconds = windowSizeInSeconds;
        }
        public bool AllowRequest()
        {
            lock (lockObject)
            {
                DateTime currentTime = DateTime.UtcNow;
                UpdateWindow(currentTime);

                if (Queue.Count < bucketSize)
                {
                    Queue.Enqueue(currentTime);
                    return true;
                }
                return false;
            }
        }

        private void UpdateWindow(DateTime currentTime)
        {
            if (Queue.IsEmpty)
            {
                return;
            }

            // Remove all timestamps which fall behind greater than windowSizeInSeconds.


            while (!Queue.IsEmpty)
            {
                Queue.TryPeek(out DateTime oldestTimeInQueue);
                double timeDifference = (currentTime - oldestTimeInQueue).TotalSeconds;
                if (timeDifference >= windowSizeInSeconds)
                {
                    Queue.TryDequeue(out DateTime oldestTimeOutQueue);
                }
                else
                {
                    break;
                }
            }

        }
    }
}
