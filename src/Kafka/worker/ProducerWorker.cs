using Kafka.model;
using Kafka.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.worker
{
    /// <summary>
    /// Worker to produce messages to a certain topic.
    /// </summary>
    internal class ProducerWorker
    {
        private TopicService topicService;
        private string TopicName;
        public ProducerWorker(TopicService topicService, string topicName) 
        {
            this.topicService = topicService;
            this.TopicName = topicName;
        }
        public void Run() 
        {
            while (true)
            {
                Message msg = new Message("Random Msg : " + Random.Shared.Next());
                int partitionId = topicService.PublishMessage(TopicName, msg);
                Console.WriteLine($"Message published to Topic: {TopicName}, PartitionId: {partitionId}");

                try
                {
                    Thread.Sleep(3000);
                }
                catch (Exception ex) {
                    Console.WriteLine($"Producer going down due to {ex.Message}");                
                }                
            }
        
        }
    }
}
