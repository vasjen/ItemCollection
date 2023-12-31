
using Common.Core.Entities;

namespace  Common.Core.Entities
{
    public class Item : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public List<Tag> Tags { get; } = new();
        public List<Comment> Comments {get;} = new List<Comment>();
        public DateTimeOffset CreatedTime { get; init; }
        public Guid CollectionId {get;set;}
        public Collection Collection {get;set;}
        }
}
