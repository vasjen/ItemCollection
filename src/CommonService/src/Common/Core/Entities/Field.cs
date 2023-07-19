

namespace  Common.Models
{
    public abstract class Field 
    {
        public long Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public long? FieldsId { get; set; } 
        public Fields? Fields { get; set; } = null!;
    }

   
}