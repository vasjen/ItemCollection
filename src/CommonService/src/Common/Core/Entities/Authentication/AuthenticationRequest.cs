using System.ComponentModel.DataAnnotations;

namespace Common.Core.Entities.Authentication{

    public class AuthenticationRequest{

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl {get;set;}
    }
}