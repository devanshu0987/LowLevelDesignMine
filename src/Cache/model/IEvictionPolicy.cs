using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    internal interface IEvictionPolicy
    {
        /// <summary>
        /// Update statistics for the Key which was accessed.
        /// </summary>
        /// <param name="key"></param>
        public void KeyAccessed(string key);

        /// <summary>
        /// Update statistics for the Key which was deleted.
        /// </summary>
        /// <param name="key"></param>
        public void KeyEvicted(string key);

        public string GetKeyToEvict();

    }
}
