using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    internal class InMemoryStorage : IStorage
    {
        private Dictionary<string, string> _storage;

        public InMemoryStorage()
        {
            _storage = new Dictionary<string, string>();
        }

        public bool ContainsKey(string key)
        {
            return _storage.ContainsKey(key);
        }


        public void AddOrUpdate(string key, string value)
        {
            _storage[key] = value;
        }

        public bool TryGet(string key, out string value)
        {
            if(ContainsKey(key))
            {
                value = _storage[key];
                return true;
            }
            else 
            {
                value = string.Empty;
                return false; 
            }
        }

        public bool TryRemove(string key)
        {
            if (ContainsKey(key))
            {
                _storage.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Count()
        {
            return _storage.Count;
        }
    }
}
