using Microsoft.AspNetCore.Identity;

namespace CollectionService.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Collection> Collections {get;}
        public List<Comment> Comments {get;}
        public List<Item> Items {get;}
    }
}