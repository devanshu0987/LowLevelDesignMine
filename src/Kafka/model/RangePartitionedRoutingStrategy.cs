using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    internal class RangePartitionedRoutingStrategy : IRoutingStrategy
    {
        public int Route(Topic topic, Message message)
        {
            int partitionCount = topic.GetPartitionCount();
            string messagePartitionId = message.GetPartitionId();
            // TODO: Hash Code should be cryptographically string and deterministic.
            int mod = messagePartitionId.GetHashCode() % partitionCount;
            if(mod < 0)
            {
                mod += partitionCount;
            }
            return mod % partitionCount;
        }
    }
}
