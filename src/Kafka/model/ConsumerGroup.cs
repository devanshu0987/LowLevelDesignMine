using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    /// <summary>
    /// A consumer group consumes one message from each of the partitions.
    /// </summary>
    internal class ConsumerGroup
    {
        private Topic topic;
        private List<Consumer> consumers;
        public ConsumerGroup(Topic topic)
        {
            this.topic = topic;
            // Generate consumer for each partition.
            consumers = new List<Consumer>();
            int partitionCount = topic.GetPartitionCount();
            foreach (var item in Enumerable.Range(0, partitionCount))
            {
                consumers.Add(new Consumer(topic, item));
            }

        }

        public List<Message> Consume()
        {
            List<Message> messages = new List<Message>();
            foreach(var consumer in consumers)
            {
                Message msg = consumer.Consume();
                if(msg != null)
                {
                    messages.Add(msg);
                }
            }
            return messages;
        }
    }
}
