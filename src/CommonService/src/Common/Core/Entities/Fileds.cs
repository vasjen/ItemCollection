

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace  Common.Models
{
    public class Fields
    {
        public long Id { get; init; }
        public List<FieldInt>? FieldInt {get;} = new List<FieldInt>();
        public List<FieldString>? FieldString {get;} = new List<FieldString>();
        public List<FieldText>? FieldText {get;} = new List<FieldText>();
        public List<FieldBool>? FieldBool {get;} = new List<FieldBool>();
        public List<FieldDate>? FieldDate {get;} = new List<FieldDate>();
        public Guid CollectionId {get;set;}
        public Collection Collection {get;set;} = null!;
    }


    public class FieldDate : Field
    {
      public  DateTime Value {get;set;}
    }

    public class FieldBool : Field
    {
      public  bool Value {get;set;}
    }

    public class FieldText : Field
    {
        public string Value {get;set;} = string.Empty;
    }

    public class FieldString : Field
    {
        [StringLength(255)]
       public string Value {get;set;} = string.Empty;
    }

    public class FieldInt : Field
    {
       public int Value {get;set;}
    }
}