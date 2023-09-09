using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    /// <summary>
    /// One consumer is for 1 partition of a topic.
    /// It needs to know about the topic and the partition.
    /// </summary>
    internal class Consumer
    {
        private int offset;
        private Topic topic;
        private int partitionId;
        // It needs to know which topic and which partition.
        public Consumer(Topic topic, int partitionId) 
        {
            offset = 0;
            this.topic = topic;
            this.partitionId = partitionId;        
        }

        public Message Consume()
        {
            Message msg = topic.GetNextMessage(partitionId, offset);
            if(msg != null)
            {
                offset++;
                return msg;
            }
            return null;
        }
    }
}
