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
    }
}