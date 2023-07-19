using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace CollectionService
{
    public record UserDto(string Id, string UserName);

    public record ItemDto(Guid Id, string NameItem, DateTimeOffset AddedDate);
    public record CreateItemDto([Required] string NameItem, List<string> Tags);
    public record UpdateItemDto([Required] string NameItem, List<string> Tags);

    public record CollectionDto(Guid Id, string NameCollection, string Description, Theme Theme, DateTimeOffset CreatedTime, Guid AuthorId);
    public record CreateCollectionDto([Required]string NameCollection,[Required] string Description, [Required]Theme Theme, Guid ApplicationUserId, Dictionary<int,string> Fields);
    public record UpdateCollectionDto([Required]string NameCollection,[Required] string Description, [Required]Theme Theme, Guid ApplicationUserId);

    public record CommentDto(string CommentText, DateTimeOffset AddedDate, string Author);
    public record CreateCommentDto(string CommentText);

    public record CreateFields(long FieldsId, List<Field> Fields);



}