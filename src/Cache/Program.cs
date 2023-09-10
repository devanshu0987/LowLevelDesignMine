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

            InMemoryStorage storage2 = new();
            RandomEvictionPolicy policy2 = new();
            KVStore cache2 = new(storage2, policy2, 2);

            cache2.Add("c", "1");
            cache2.Add("a", "1");
            cache2.Add("a", "2");
            cache2.Add("b", "3");
            cache2.Add("b", "4");
        }
    }
}