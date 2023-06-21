using System.Reflection;

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
            }
            catch
            {
                return false;
            }

        }
        else if (propInfo.PropertyType == typeof(int))
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
