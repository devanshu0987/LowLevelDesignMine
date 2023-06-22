using System.Text.Json;

namespace FilterUnitTests
{
    [TestClass]
    public class GenericFilterUnitTest
    {
        [TestMethod]
        public void FilterByIdEqualToZero()
        {
            var transactions = Helper.GenerateDummyTransactions();
            GenericFilter idFilterValueZero = new GenericFilter("Id", 0);
            var filteredTrx = transactions.Where(x => idFilterValueZero.Test(x)).ToList();
            Assert.IsNotNull(filteredTrx);
            Assert.IsTrue(filteredTrx.Count == 1);
            Assert.IsTrue(filteredTrx[0].Id == 0);
        }

        [TestMethod]
        public void MultipleFilters()
        {
            //Dictionary<string, string> f = new Dictionary<string, string>
            //{
            //    { "userId", "1" },
            //    { "currency", "1" },
            //    { "timestamp", "1||3" }
            //};
            var temp = JsonSerializer.Deserialize<List<Trx>>(File.ReadAllText("Inputs/inputs.json"));
            GenericFilter userIdFilter = new GenericFilter("UserId", 0);
        }
    }
}