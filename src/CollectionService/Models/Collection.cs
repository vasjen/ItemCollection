using CollectionService.Models;
using CollectionService.Models.Entities;

namespace  CollectionService.Models
{
    public class Collection : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public List<Item> Items { get; } = new();
        public DateTimeOffset CreatedTime { get; init; }
        public string Description { get; set; } = string.Empty;
    }
}