using Kafka.model;
using Kafka.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.service
{
    internal class TopicService
    {
        TopicRepository topicRepository;
        public TopicService() 
        { 
            topicRepository = new TopicRepository();        
        }

        public Topic CreateTopic(string topicName, int partitionCount, IRoutingStrategy routingStrategy)
        {
            // TODO: Guard that topicName and partitionCount are valid.
            Topic topic = topicRepository.GetTopic(topicName);
            if(topic == null)
            {
                Topic newTopic = new Topic(topicName, partitionCount, routingStrategy);
                topicRepository.Add(newTopic);
                return newTopic;
            }
            return topicRepository.GetTopic(topicName);
        }

        public Topic GetTopic(string topicName)
        {
            Topic topic = topicRepository.GetTopic(topicName);
            if (topic == null)
            {
                return null;
            }
            return topic;
        }

        public int PublishMessage(string topicName, Message msg)
        {
            Topic topic = topicRepository.GetTopic(topicName);
            if (topic == null)
            {
                return -1;
            }
            return topic.PublishMessage(msg);
        }
    }
}
