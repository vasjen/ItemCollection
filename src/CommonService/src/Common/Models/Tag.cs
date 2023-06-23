namespace  Common.Models
{
    public class Tag
    {
        public Guid Id {get;init;}
        public string Name {get;set;} = string.Empty;
        public List<Item> Items { get; } = new();


    }
}