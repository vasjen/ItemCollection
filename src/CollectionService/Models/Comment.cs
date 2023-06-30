using CollectionService.Models;
using CollectionService.Models.Entities;

namespace  CollectionService.Models
{
    public class Comment : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get => Value; set => Value = value; }
        public DateTimeOffset CreatedTime {get;init;}
        public string Value { get; set; } = string.Empty;
        public Guid ItemId {get;set;}
        public Item Item {get;set;} = null!;
        public Guid ApplicationUserId {get;set;}
        public ApplicationUser User {get;set;} = null!;
    }

 
}