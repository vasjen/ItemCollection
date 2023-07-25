using Common.Core.Entities;

namespace  Common.Core.Entities
{
    public class Tag : IEntity
    {
        public Guid Id {get;init;}
        public string Name {get;set;} = string.Empty;
        public DateTimeOffset CreatedTime {get;init;} = DateTimeOffset.Now;
        
    }
}