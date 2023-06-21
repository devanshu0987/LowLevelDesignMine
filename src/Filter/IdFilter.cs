public class IdFilter
{
    public IdFilter(int value)
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
