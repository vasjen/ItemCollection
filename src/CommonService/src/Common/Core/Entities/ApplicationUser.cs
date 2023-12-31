using Microsoft.AspNetCore.Identity;

namespace Common.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Collection> Collections {get;} = new List<Collection>();
        public List<Comment> Comments {get;} = new List<Comment>();

        public bool? IsActive {get;set;}
        public bool? IsDarkMode {get;set;}
        
    }
}