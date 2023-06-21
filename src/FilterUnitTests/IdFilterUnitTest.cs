namespace FilterUnitTests
{
    [TestClass]
    public class IdFilterUnitTest
    {
        [TestMethod]
        public void FilterByZero()
        {
            var transactions = Helper.GenerateDummyTransactions();
            IdFilter filterZero = new(0);
            var filteredTrx = transactions.Where(x => filterZero.Test(x)).ToList();
            Assert.IsNotNull(filteredTrx);
            Assert.IsTrue(filteredTrx.Count == 1);
            Assert.IsTrue(filteredTrx[0].Id == 0);
        }
    }
}