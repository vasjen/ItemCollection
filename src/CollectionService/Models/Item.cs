namespace  CollectionService.Models
{
    public class Item : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public List<Tag> Tags { get; } = new();
        public int Quantity { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}