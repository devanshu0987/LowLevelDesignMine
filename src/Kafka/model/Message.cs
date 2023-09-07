﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.model
{
    internal class Message
    {
        int Id;
        string Body;
        // per message partitionId.
        private string PartitionId = string.Empty; 
        Dictionary<string, string> Metadata;

        public Message(string body) 
        {
            Body = body;
            Metadata = new Dictionary<string, string>();
        }

        public void AddMetadata(string key, string value)
        {
            Metadata[key] = value;
        }

        public void SetPartitionId(string partitionId)
        {
            PartitionId = partitionId;
        }

        public string GetPartitionId()
        {
            return PartitionId;
        }
    }
}