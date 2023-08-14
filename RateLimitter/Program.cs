namespace RateLimitter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserIdentificationService service = new UserIdentificationService();

            foreach(var item in Enumerable.Range(1, 10))
            {
                string response = service.ServeRequest("ABC");
                Console.WriteLine(response);
            }

            foreach (var item in Enumerable.Range(1, 10))
            {
                string response = service.ServeRequest("DEF");
                Console.WriteLine(response);
            }

            //Parallel.ForEach(Enumerable.Range(1, 10), (int i) =>
            //{
            //    Thread.Sleep(1000);
            //    string key = Random.Shared.Next() % 2 == 0 ? "ABC" : "DEF";
            //    string response = service.ServeRequest(key);
            //    Console.WriteLine(response);
            //});
        }
    }
}