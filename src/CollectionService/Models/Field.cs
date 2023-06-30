using CollectionService.Models;
using CollectionService.Models.Entities;

namespace  CollectionService.Models
{
    public class Field 
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public FieldType Type {get;set;}
    }

    public enum FieldType 
    {Integer,String,MultilineText,Boolean,Date}
}