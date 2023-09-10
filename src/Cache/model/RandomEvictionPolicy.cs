using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    internal class RandomEvictionPolicy : IEvictionPolicy
    {
        /// <summary>
        /// Dictionary is not ideal. We dont need to store their frequency of elements, nor we need to maintain it.
        /// TODO: Reduce space.
        /// </summary>
        private Dictionary<string, int> keys;
        public RandomEvictionPolicy() 
        {
            keys = new Dictionary<string, int>();
        }

        public string GetKeyToEvict()
        {
            return keys.Keys.First();
        }

        public void KeyAccessed(string key)
        {
            if(!keys.ContainsKey(key))
            {
                keys.Add(key, 1);
            }
        }

        public void KeyEvicted(string key)
        {
            if (keys.ContainsKey(key))
            {
                keys.Remove(key);
            }
        }
    }
}
