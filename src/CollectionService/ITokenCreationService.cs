using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity;


namespace CollectionService.Services{
    public interface ITokenCreationService
    {
        AuthenticationResponse CreateToken(IdentityUser user);
        KeyValuePair<string,string> GetUserFromToken(string Bearer);   
    }
}