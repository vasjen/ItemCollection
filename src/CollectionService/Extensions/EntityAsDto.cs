

using CollectionService.Models;

namespace CollectionService.Extensions
{
    public static class EntityAsDto
    {
        public static ItemDto AsDto(this Item item)
        {
            
            return new ItemDto(item.Id, item.Name, item.CreatedTime);
        }
        public static CollectionDto AsDto(this Collection collection)
        {
            var items = collection.Items.ToList();
            return new CollectionDto(collection.Id, collection.Name, collection.Description, items.Select(p => p.AsDto()), collection.Theme.ToString(), collection.CreatedTime);
        }
        public static UserDto AsDto(this ApplicationUser user)
        {
            return new UserDto(user.Id.ToString(), user.UserName);
        }
        public static CommentDto AsDto(this Comment comment)
        {
            return new CommentDto(comment.Name, comment.CreatedTime);
        }
    
    }
}