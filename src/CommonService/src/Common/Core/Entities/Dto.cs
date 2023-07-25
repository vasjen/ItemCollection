using System.ComponentModel.DataAnnotations;
using Common.Core.Entities;

namespace Common.Core.Entities
{
    public record UserDto(string Id, string UserName);

    public record ItemDto(Guid Id, string NameItem, DateTimeOffset AddedDate, string CollectionName);
    public record CreateItemDto([Required] string NameItem, FieldsDto Fields);
    public record UpdateItemDto([Required] string NameItem, List<string> Tags);

    public record CollectionDto(Guid Id, string NameCollection, string Description, Theme Theme, DateTimeOffset CreatedTime, Guid AuthorId);
    public record CreateCollectionDto([Required]string NameCollection,[Required] string Description, [Required]Theme Theme, Guid ApplicationUserId, Dictionary<string,int> Fields);
    public record UpdateCollectionDto([Required]string NameCollection,[Required] string Description, [Required]Theme Theme, Guid ApplicationUserId);

    public record CommentDto(string CommentText, DateTimeOffset AddedDate, string Author);
    public record CreateCommentDto(string CommentText);

    public record CreateFields(long FieldsId, List<Field> Fields);

    public record FieldsDto (List<FieldInt> FieldsInt, List<FieldBool> FieldsBool, List<FieldString> FieldsString, List<FieldText> FieldsText, List<FieldDate> FieldsDate);

    
}
public class ResponseDataCollection
{
    public List<Collection> Values { get; set; }
}
public class ResponseDataItem
{
    public List<Item> Values { get; set; }
}