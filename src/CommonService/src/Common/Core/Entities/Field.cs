

namespace  Common.Core.Entities
{
    public abstract class Field 
    {
        public long Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public long? FieldsId { get; set; } 
        public Guid ItemId { get; set; } 
        public Fields? Fields { get; set; } = null!;
    }

   
}