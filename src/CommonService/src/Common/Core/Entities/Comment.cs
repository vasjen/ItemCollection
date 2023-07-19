
using Common.Core.Entities;

namespace  Common.Models
{
    public class Comment : IEntity
    {
        public Guid Id { get; init; }
        public string Name {get;set;} = string.Empty;
        public DateTimeOffset CreatedTime {get;init;}
        public Guid ItemId {get;set;}
        public Item Item {get;set;} = null!;

        public Guid ApplicationUserId {get;set;}
        public ApplicationUser User {get;set;}
    }

 
}