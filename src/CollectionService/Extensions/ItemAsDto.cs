using CollectionService;
using CollectionService.Models;

namespace CollectionService.Extensions
{
    public static class ItemAsDto
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, new List<string>(), item.Quantity, item.CreatedDate);
        }
    }
}