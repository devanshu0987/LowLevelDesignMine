using Kafka.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.repository
{
    internal class TopicRepository
    {
        Dictionary<string, Topic> Topics;
        public TopicRepository() 
        {
            Topics = new Dictionary<string, Topic>();        
        }

        public void Add(Topic topic)
        {
            if(Topics.ContainsKey(topic.Name))
            {
                return;
            }
            Topics.Add(topic.Name, topic);
        }

        public Topic GetTopic(string topicName)
        {
            if (Topics.ContainsKey(topicName))
            {
                return Topics[topicName];
            }
            // TODO: Return error.
            return null;
        }
    }
}
