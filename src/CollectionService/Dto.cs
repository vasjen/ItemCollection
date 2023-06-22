using System.ComponentModel.DataAnnotations;

namespace CollectionService
{
    public record ItemDto(Guid Id, string NameItem, string[] Tags, int Quantity, DateTimeOffset AddedDate);
    public record CreateItemDto([Required] string NameItem, string[] Tags, [Range(1,int.MaxValue)] int Quantity);
    public record UpdateItemDto([Required] string NameItem, string[] Tags, [Range(1,int.MaxValue)] int Quantity);
}