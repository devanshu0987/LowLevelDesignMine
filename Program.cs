using Bogus;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

int id = 0;

//You have a set of transactions in DB where every transaction has these properties

//id
//timestamp
//currency
//userId
//amount

//Write the endpoint through which we can get transactions filtered by userId, currency or between a timestamp
var currencies = new List<string> { "DOL", "EUR", "SGD" };
var testTransactions = new Faker<Trx>()
    .StrictMode(true)
    .RuleFor(x => x.id, f => id++)
    .RuleFor(x => x.time, f => f.Random.Int(1))
    .RuleFor(x => x.currency, f => f.PickRandom(currencies))
    .RuleFor(x => x.userId, f => f.Internet.UserName())
    .RuleFor(x => x.amount, f => f.Random.Int(1,100));

var transactions = testTransactions.Generate(5);
foreach (var item in transactions)
{
    Console.WriteLine(JsonSerializer.Serialize(item));
}

// filter via Id
// For each field, implement a filter. => We need to implement so many things. => Can we make it generic?
Id_Filter filterZero = new Id_Filter(0);
var filteredTrx = transactions.Where(x => filterZero.Test(x)).ToList();
Console.WriteLine("Zero Filter in action");
foreach (var item in filteredTrx)
{
    Console.WriteLine(JsonSerializer.Serialize(item));
}

Dictionary<string, string> f = new Dictionary<string, string>
{
    { "userId", "1" },
    { "currency", "1" },
    { "timestamp", "1||3" }
};

var userIdFiltering = new Filter(1, 2);

class Id_Filter
{
    public Id_Filter(int value)
    {
        Id = value;
    }
    
    // private because we dont want to change value mid way.
    private int Id { get; set; }
    public bool Test(Trx t)
    {
        return t.id == Id;
    }
}

class Filter
{
    public Filter(int startTime, int endTime)
    {
              
    }

    List<Trx> ApplyFilter(List<Trx> t)
    {
                
        return t;
    }
}

class Trx
{
    public int id { get; set; }
    public int time { get; set; }
    public string currency { get; set; }
    public string userId { get; set; }
    public int amount { get; set; }
}
