using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterUnitTests
{
    [TestClass]
    public class FilterParityUnitTests
    {
        [TestMethod]
        public void FilterByIdEqualToZeroIsSameBetweenSpecificAndGenericFilter()
        {
            var transactions = Helper.GenerateDummyTransactions();

            IdFilter filterZero = new(0);
            var transactionsPostSpecificFilter = transactions.Where(x => filterZero.Test(x)).ToList();

            GenericFilter idFilterValueZero = new GenericFilter("Id", 0);
            var transactionsPostGenericFilter = transactions.Where(x => idFilterValueZero.Test(x)).ToList();

            Assert.IsNotNull(transactionsPostSpecificFilter);
            Assert.IsNotNull(transactionsPostGenericFilter);

            Assert.IsTrue(transactionsPostSpecificFilter.Count == 1 && transactionsPostGenericFilter.Count == 1);
            Assert.IsTrue(transactionsPostSpecificFilter[0].Id == 0 && transactionsPostSpecificFilter[0].Id == transactionsPostGenericFilter[0].Id);
        }
    }
}
