using System.ComponentModel.DataAnnotations;
using CollectionService.Models;

namespace CollectionService
{
    public record UserDto(string Id, string UserName);

    public record ItemDto(Guid Id, string NameItem, DateTimeOffset AddedDate);
    public record CreateItemDto([Required] string NameItem, List<string> Tags);
    public record UpdateItemDto([Required] string NameItem, List<string> Tags);

    public record CollectionDto(Guid Id, string NameCollection, string Description, IEnumerable<ItemDto> Items,string Theme, DateTimeOffset CreatedTimea);
    public record CreateCollectionDto([Required]string NameCollection,[Required] string Description, Theme Theme, IEnumerable<ItemDto> Items);
    public record UpdateCollectionDto([Required]string NameCollection,[Required] string Description, Theme Theme, IEnumerable<ItemDto> Items);

    public record CommentDto(string CommentText, DateTimeOffset AddedDate, string Author);
    public record CreateCommentDto(string CommentText);



}