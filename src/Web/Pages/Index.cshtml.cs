using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public IEnumerable<Collection?>? Collections {get;set;}
    public IEnumerable<Item?>? Items {get;set;}
    public IEnumerable<Comment?>? Comments {get;set;}

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
       var authClient = _httpClientFactory.CreateClient("CollectionService");
       var disco = await authClient.GetDiscoveryDocumentAsync("http://gateway/identity");
       
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
       var response = await authClient.GetAsync("collection/collection");
       System.Console.WriteLine("Response: \n");
       System.Console.WriteLine(await response.Content.ReadAsStringAsync());
       if (response.IsSuccessStatusCode)
       {
            var colllection = await response.Content.ReadFromJsonAsync<List<Collection>>();
            Collections = colllection.OrderByDescending(p => p.CreatedTime).Take(4).ToList();
       }

       response = await authClient.GetAsync("collection/item");
       System.Console.WriteLine("Response: \n");
       System.Console.WriteLine(await response.Content.ReadAsStringAsync());
       if (response.IsSuccessStatusCode)
       {
            var items = await response.Content.ReadFromJsonAsync<List<Item>>();
            Items = items.OrderByDescending(p => p.CreatedTime).Take(5);
       }
       response = await authClient.GetAsync("collection/item/GetComents");
       if (response.IsSuccessStatusCode)
       {
            var comments = await response.Content.ReadFromJsonAsync<List<Comment>>();
            Comments = comments.OrderByDescending(p => p.CreatedTime).Take(5);
       }
       
        
    }
}
