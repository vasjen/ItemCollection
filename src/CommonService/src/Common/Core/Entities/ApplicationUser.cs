using Microsoft.AspNetCore.Identity;

namespace Common.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Collection> Collections {get;} = new List<Collection>();
        public List<Comment> Comments {get;} = new List<Comment>();
        
    }
}