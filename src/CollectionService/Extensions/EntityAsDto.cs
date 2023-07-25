



using Common.Core.Entities;

namespace CollectionService.Extensions
{
    public static class EntityAsDto
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.CreatedTime, item.Collection.Name);
        }
        public static CollectionDto AsDto(this Collection collection)
        {
            var items = collection.Items.ToList();
            return new CollectionDto(collection.Id, collection.Name, collection.Description, collection.Theme, collection.CreatedTime, collection.ApplicationUserId);
        }
        public static UserDto AsDto(this ApplicationUser user)
        {
            return new UserDto(user.Id.ToString(), user.UserName);
        }
       
    
    }
}