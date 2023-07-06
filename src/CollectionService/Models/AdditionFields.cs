using CollectionService.Models;

namespace  CollectionService.Models
{
    public class CustomFields 
    {
        public int Id { get; init; }
        public List<UserField> Fields {get;set;}
    }

    public class UserField
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public FieldValue Value { get; set; }

    }
    public abstract class FieldValue
    {
        
    }

    public class BoolFieldValue : FieldValue
    {
        public bool Value { get; set; }
    }

    public class DateFieldValue : FieldValue
    {
        public DateTime Value { get; set; }
    }

    public class IntFieldValue : FieldValue
    {
        public int Value { get; set; }
    }

    public class StringFieldValue : FieldValue
    {
        public string Value { get; set; }
    }
   
   
}