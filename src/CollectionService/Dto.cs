using System.ComponentModel.DataAnnotations;

namespace CollectionService
{
    public record ItemDto(Guid Id, string NameItem, List<string> Tags, int Quantity, DateTimeOffset AddedDate);
    public record CreateItemDto([Required] string NameItem, List<string> Tags, [Range(1,int.MaxValue)] int Quantity);
    public record UpdateItemDto([Required] string NameItem, List<string> Tags, [Range(1,int.MaxValue)] int Quantity);

    public record CollectionDto(Guid Id, string NameCollection, string Description, IEnumerable<ItemDto> Items, DateTimeOffset CreatedTime);


}