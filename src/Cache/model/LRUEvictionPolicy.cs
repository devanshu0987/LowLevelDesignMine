using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    /// <summary>
    /// Least recently used.
    /// </summary>
    internal class LRUEvictionPolicy : IEvictionPolicy
    {
        // Stores the Keys in LRU order.
        private LinkedList<string> keys;
        // Stores Key to LinkedListNode mapping.
        private Dictionary<string, LinkedListNode<string>> mapper;

        public LRUEvictionPolicy()
        {
            keys = new LinkedList<string>();
            mapper = new Dictionary<string, LinkedListNode<string>>();
        }
        /// <summary>
        /// When you access, move the accessed key to the start.
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void KeyAccessed(string key)
        {
            // when you access a key, you need to first find if it is already present.
            if(mapper.ContainsKey(key))
            {
                // then we need to move the key from where ever it is to the front.
                LinkedListNode<string> node = mapper[key];
                keys.Remove(node);
                LinkedListNode<string> insertedNode = keys.AddFirst(key);
                mapper[key] = insertedNode;
            }
            else
            {
                LinkedListNode<string> insertedNode = keys.AddFirst(key);
                mapper[key] = insertedNode;
            }
        }
        
        /// <summary>
        /// When key is evicted, we need to remove the reference from our state.
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="NotImplementedException"></exception>

        public void KeyEvicted(string key)
        {
            if(mapper.ContainsKey(key))
            {
                LinkedListNode<string> node = mapper[key];
                keys.Remove(node);
                // clean mapper as well.
                mapper.Remove(key);
            }
            // else, no action.
        }

        /// <summary>
        /// End of the queue can be removed.
        /// </summary>
        /// <returns></returns>
        public string GetKeyToEvict()
        {
            if (keys.Count > 0)
                return keys.Last.Value;
            else
                return string.Empty;
        }
    }
}
