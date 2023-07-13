using Common.Core.Entities;

namespace  Common.Models
{
    public class Tag : IEntity
    {
        public Guid Id {get;init;}
        public string Name {get;set;} = string.Empty;
        public DateTimeOffset CreatedTime {get;init;} = DateTimeOffset.Now;
        
    }
}