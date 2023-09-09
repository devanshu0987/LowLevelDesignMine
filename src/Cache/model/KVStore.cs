using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    internal class KVStore : ICache
    {
        private IStorage storage;
        private IEvictionPolicy policy;
        private int capacity;

        public KVStore(IStorage storage, IEvictionPolicy policy, int capacity)
        {
            this.storage = storage;
            this.policy = policy;
            this.capacity = capacity;
        }

        public bool Add(string key, string value)
        {
            if(storage.Count() >= capacity)
            {
                string keyToEvict = policy.GetKeyToEvict();
                bool status = storage.TryRemove(keyToEvict);
                if(status)
                {
                    // we were able to remove the key.
                    policy.KeyEvicted(keyToEvict);
                }
                else
                {
                    // we were not able to evict using the current policy and we still have capcity overload.
                    // Multiple options: throw error||ignore the Add||evict random key which is guaranteed to be successful.
                    // throw new Exception("Capacity overload. Eviction Policy not able to evict keys");
                    return false;
                }
            }

            // If you are here, you have been able to make space for the key.
            storage.AddOrUpdate(key, value);
            policy.KeyAccessed(key);
            return true;
        }

        public string Get(string key)
        {
            bool status = storage.TryGet(key, out var value);
            if(status)
            {
                return value;
            }
            return string.Empty;
        }

        public void Remove(string key)
        {
            bool status = storage.TryRemove(key);
            if(status)
            {
                policy.KeyEvicted(key);
            }
        }
    }
}
