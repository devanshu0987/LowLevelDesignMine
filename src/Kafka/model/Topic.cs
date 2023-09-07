using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    internal class Topic
    {
        private int Id;
        public string Name;
        private List<Partition> Partitions;
        private int PartitionCount;
        private IRoutingStrategy RoutingStrategy;
        public Topic(string name, int partitionCount, IRoutingStrategy routingStrategy) 
        {
            PartitionCount = partitionCount;
            Partitions = new List<Partition>(PartitionCount);
            RoutingStrategy = routingStrategy;
        }

        public int GetPartitionCount()
        {
            return PartitionCount;
        }

        private Partition GetPartition(int partition)
        {
            if(partition < PartitionCount)
            {
                return Partitions[partition];
            }
            // TODO: I dont like returning nulls.
            return null;
        }

        public void PublishMessage(Message message)
        {
            int partitionIdToRouteMessageTo = RoutingStrategy.Route(this, message);
            Partition partition = GetPartition(partitionIdToRouteMessageTo);
            partition.AddMessage(message);
        }
    }
}
