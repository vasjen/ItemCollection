using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin;

public class IndexModel : PageModel
{
    public List<ApplicationUser> ApplicationUsers {get;set;}
    private readonly ILogger<SignupModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public string Token {get;set;}

    public IndexModel(ILogger<SignupModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task OnGetAsync()
    {
        var authClient = _httpClientFactory.CreateClient("CollectionService");
        var disco = await authClient.GetDiscoveryDocumentAsync("https://localhost:7195");
       
          var tokenRespone = authClient.RequestClientCredentialsTokenAsync(
           new ClientCredentialsTokenRequest
           {
               Address = disco.TokenEndpoint,
               ClientId = "client_id",
               ClientSecret = "client_secret",
               Scope = "CollectionApi"
           } 
       ).GetAwaiter().GetResult();
       authClient.SetBearerToken(tokenRespone.AccessToken);
       Token = tokenRespone.AccessToken;
       var response = await authClient.GetAsync("identity/User");
        System.Console.WriteLine("Response: \n");
       System.Console.WriteLine(await response.Content.ReadAsStringAsync());
       if (response.IsSuccessStatusCode)
       {
            ApplicationUsers = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
       }
    }
}

