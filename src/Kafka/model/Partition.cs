using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    internal class Partition
    {
        List<Message> Messages;
        int Id;

        public Partition(int id)
        {
            this.Id = id;
            Messages = new List<Message>();
        }

        public bool AddMessage(Message message)
        {
            // TODO: Make it thread safe.
            // TODO: It is possible that a partition could overflow. We need a limit.
            // TODO: We can add metadata wrt partitionId here.
            message.AddMetadata("partitionId", Id.ToString());  
            Messages.Add(message);
            return true;
        }

        public Message GetMessage(int offset)
        {
            // TODO: Do proper error handling here. We are returning null here.
            if (offset < 0 || offset >= Messages.Count)
                return null;
            return Messages[offset];
        }
    }
}
