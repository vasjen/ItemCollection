namespace  CollectionService.Models
{
    public class Item
    {
        public Guid Id {get;init;}
        public string Name {get;set;} = string.Empty;
        public List<Tag> Tags { get; } = new();
        public Guid TagId {get;set;}
        public int Quantity {get;set;}
        public DateTimeOffset CreatedDate {get;set;}

    }
}