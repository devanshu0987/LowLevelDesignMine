using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    /// <summary>
    /// Provides the partition Id where to send the message.
    /// </summary>
    internal interface IRoutingStrategy
    {
        public int Route(Topic topic, Message message);
    }
}
