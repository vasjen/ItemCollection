using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Personal;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private string identityUrl;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        identityUrl = configuration.GetValue<string>("Identity");
    }
    
    public async Task OnGetAsync()
    {
       var authClient = _httpClientFactory.CreateClient("CollectionService");
       var disco = await authClient.GetDiscoveryDocumentAsync("http://localhost:1000/");
       
          var tokenRespone = authClient.RequestClientCredentialsTokenAsync(
           new ClientCredentialsTokenRequest
           {
               Address = disco.TokenEndpoint,
               ClientId = "client_id",
               ClientSecret = "client_secret",
               Scope = "CollectionApi"
           } 
       ).GetAwaiter().GetResult();
      var itemClient = _httpClientFactory.CreateClient("CollectionService"); 
      itemClient.SetBearerToken(tokenRespone.AccessToken);  
      var id = HttpContext.User.Claims.Where(p => p.Type == "sub").Select(p => p.Value).First();
     
      var response = await itemClient.GetAsync($"Collection/Collection/GetAllUser/{id}");
       System.Console.WriteLine(response.StatusCode);
       if (!response.IsSuccessStatusCode)
           ViewData["Message"] = response.StatusCode;
       else
       {
           var message = await response.Content.ReadAsStringAsync();
           ViewData["Message"] = message;
       }
        // var Collections = 
    }
}

