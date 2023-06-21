using Bogus;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

int id = 0;

//You have a set of transactions in DB where every transaction has these properties

//id
//timestamp
//currency
//userId
//amount

//Dictionary<string, string> f = new Dictionary<string, string>
//{
//    { "userId", "1" },
//    { "currency", "1" },
//    { "timestamp", "1||3" }
//};

//Write the endpoint through which we can get transactions filtered by userId, currency or between a timestamp
var currencies = new List<string> { "DOL", "EUR", "SGD" };
var testTransactions = new Faker<Trx>()
    .StrictMode(true)
    .RuleFor(x => x.Id, f => id++)
    .RuleFor(x => x.Time, f => f.Random.Int(1))
    .RuleFor(x => x.Currency, f => f.PickRandom(currencies))
    .RuleFor(x => x.UserId, f => f.Internet.UserName())
    .RuleFor(x => x.Amount, f => f.Random.Int(1,100));

var transactions = testTransactions.Generate(5);
foreach (var item in transactions)
{
    Console.WriteLine(JsonSerializer.Serialize(item));
}

// filter via Id
// For each field, implement a filter. => We need to implement so many things. => Can we make it generic?
Id_Filter filterZero = new(0);
var filteredTrx = transactions.Where(x => filterZero.Test(x)).ToList();

// Can we make a Generic Filter?
// To what level of generic: Let keep Object to be Trx itself for now. But get Value via Reflection.
// To What level of get do we support? Just top level Values.

GenericFilter idFilterValueZero = new GenericFilter("Id", 0);
var filteredTrx_Generic = transactions.Where(x => idFilterValueZero.Test(x)).ToList();

// Assert these two to be same.
Debug.Assert(filteredTrx.Count == filteredTrx_Generic.Count);
Debug.Assert(filteredTrx.Count == 1);
Debug.Assert(filteredTrx[0].Id == filteredTrx_Generic[0].Id && filteredTrx[0].UserId == filteredTrx_Generic[0].UserId);

public class GenericFilter
{
    string Key { get; set; }
    Object Value { get; set; }

    // We dont know what the data type of the value can be.
    // Lets pass in as Object for now.
    public GenericFilter(string key, Object value)
    {
        Key = key;
        Value = value;
    }

    public bool Test(Trx trx)
    {
        // get value via Reflection for the Key
        // match type of the Value with the Key's type.
        Type objectType = trx.GetType();
        PropertyInfo? propInfo = objectType.GetProperty(Key);

        if (propInfo == null)
        {
            // we couldnt find the property, hence we should return false
            return false;
        }
        // if it exists, then we can check.
        if (propInfo.PropertyType == typeof(string))
        {
            // it should be possible to type cast to string
            try
            {
                string expectedValue = (string)Value;
                string? originalValue = (string?)propInfo.GetValue(trx, null);
                return originalValue == expectedValue;
            } catch {
                return false;
            }             
            
        }
        else if(propInfo.PropertyType == typeof(int))
        {           
            // it should be possible to type cast to int
            try
            {
                int expectedValue = (int)Value;
                int? originalValue = (int?)propInfo.GetValue(trx, null);
                return originalValue == expectedValue;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

public class Id_Filter
{
    public Id_Filter(int value)
    {
        Id = value;
    }
    
    // private because we dont want to change value mid way.
    private int Id { get; set; }
    public bool Test(Trx t)
    {
        return t.Id == Id;
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

public class Trx
{
    public int Id { get; set; }
    public int Time { get; set; }
    public string? Currency { get; set; }
    public string? UserId { get; set; }
    public int Amount { get; set; }
}
