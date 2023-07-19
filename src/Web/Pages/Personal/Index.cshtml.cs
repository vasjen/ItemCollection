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

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task OnGetAsync()
    {
       var authClient = _httpClientFactory.CreateClient();
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
      var itemClient = _httpClientFactory.CreateClient(); 
      itemClient.SetBearerToken(tokenRespone.AccessToken);  
      var id = HttpContext.User.Claims.Where(p => p.Type == "sub").Select(p => p.Value).First();
     
      var response = await itemClient.GetAsync($"http://localhost:5254/Collection/GetAllUser/{id}");
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

