using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    internal class RoundRobinRoutingStrategy : IRoutingStrategy
    {
        public int Route(Topic topic, Message message)
        {
            int partitionCount = topic.GetPartitionCount();
            return Random.Shared.Next(0, partitionCount);
        }
    }
}
