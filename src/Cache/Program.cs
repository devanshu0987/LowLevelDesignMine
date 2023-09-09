using Cache.model;

namespace Cache
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InMemoryStorage inMemoryStorage = new();
            LRUEvictionPolicy policy = new();
            KVStore cache = new(inMemoryStorage, policy, 2);

            cache.Add("c", "1");
            cache.Add("a", "1");
            cache.Add("a", "2");
            cache.Add("b", "3");
            cache.Add("b", "4");
        }
    }
}