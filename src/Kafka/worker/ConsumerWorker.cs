using Kafka.model;
using Kafka.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.worker
{
    internal class ConsumerWorker
    {
        private ConsumerGroup consumerGroup;

        public ConsumerWorker(ConsumerGroup consumerGroup)
        {
            this.consumerGroup = consumerGroup;
        }

        public void Run()
        {
            while(true)
            {
                var messages = consumerGroup.Consume();
                foreach (var message in messages)
                {
                    Console.WriteLine("Consumed message from : " + message.GetMetadata("partitionId"), ConsoleColor.Red);
                }

                try
                {
                    Thread.Sleep(7000);
                } catch { }
            }
        }
    }
}
