using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    internal class Topic
    {
        public string Name;
        private List<Partition> Partitions;
        private int PartitionCount;
        private IRoutingStrategy RoutingStrategy;
        public Topic(string name, int partitionCount, IRoutingStrategy routingStrategy) 
        {
            Name = name;
            PartitionCount = partitionCount;
            Partitions = new List<Partition>(PartitionCount);
            for(int i = 0; i < PartitionCount; i++)
            {
                Partitions.Add(new Partition(i));
            }
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
            throw new KeyNotFoundException(partition.ToString());
        }

        public int PublishMessage(Message message)
        {
            int partitionIdToRouteMessageTo = RoutingStrategy.Route(this, message);
            Partition partition = GetPartition(partitionIdToRouteMessageTo);
            partition.AddMessage(message);
            return partitionIdToRouteMessageTo;
        }

        public Message GetNextMessage(int partitionId, int offset)
        {
            Partition partition = GetPartition(partitionId);
            return partition.GetMessage(offset);
        }
    }
}
