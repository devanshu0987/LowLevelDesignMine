using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterUnitTests
{
    internal class Helper
    {
        public static List<Trx> GenerateDummyTransactions(int count=5)
        {
            int id = 0;
            var currencies = new List<string> { "DOL", "EUR", "SGD" };
            var testTransactions = new Faker<Trx>()
                .StrictMode(true)
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.Time, f => f.Random.Int(1))
                .RuleFor(x => x.Currency, f => f.PickRandom(currencies))
                .RuleFor(x => x.UserId, f => f.Internet.UserName())
                .RuleFor(x => x.Amount, f => f.Random.Int(1, 100));

            return testTransactions.Generate(count);
        }
    }
}
