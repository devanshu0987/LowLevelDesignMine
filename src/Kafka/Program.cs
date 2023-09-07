using Kafka.model;
using Kafka.service;
using Kafka.worker;

namespace Kafka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TopicService topicService = new TopicService();
            topicService.CreateTopic("RoundRobin", 16, new RoundRobinRoutingStrategy());
            topicService.CreateTopic("RangeHash", 16, new RangePartitionedRoutingStrategy());

            ProducerWorker worker1 = new ProducerWorker(topicService, "RoundRobin");
            ProducerWorker worker2 = new ProducerWorker(topicService, "RangeHash");
            ConsumerGroup consumerGroup = new ConsumerGroup(topicService.GetTopic("RoundRobin"));
            ConsumerWorker consumerWorker = new ConsumerWorker(consumerGroup);

            List<Task> tasks = new(2)
            {
                Task.Run(() => worker1.Run()),
                Task.Run(() => worker2.Run()),
                Task.Run(() => consumerWorker.Run()),
            };

            Task.WaitAll(tasks.ToArray());
        }
    }
}