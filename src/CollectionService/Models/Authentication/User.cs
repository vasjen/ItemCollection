using System.ComponentModel.DataAnnotations;

namespace CollectionService.Models.Authentication{
    public class User {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

