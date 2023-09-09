using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    internal interface IStorage
    {
        public void AddOrUpdate(string key, string value);
        public bool TryGet(string key, out string value);
        public bool TryRemove(string key);
        /// <summary>
        /// Returns count of keys in storage.
        /// </summary>
        /// <returns></returns>
        public int Count();
    }
}
