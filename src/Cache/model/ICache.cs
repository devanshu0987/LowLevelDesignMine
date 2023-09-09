using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.model
{
    internal interface ICache
    {
        public bool Add(string key, string value);
        public string Get(string key);
        public void Remove(string key);
    }
}
