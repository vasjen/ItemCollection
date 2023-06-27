

using CollectionService.Models;

namespace CollectionService.Extensions
{
    public static class ItemAsDto
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, new List<string>(), item.Quantity, item.CreatedTime);
        }
         public static CollectionDto AsDto(this Collection collection)
        {
            var items = collection.Items.Select(p => p.AsDto());
            return new CollectionDto(collection.Id, collection.Name, collection.Description, items, collection.CreatedTime);
        }
    
    }
}