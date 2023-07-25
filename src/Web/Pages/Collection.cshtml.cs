using System.Text.Json;
using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class CollectionModel : PageModel
{
    private readonly ILogger<CollectionModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public Collection Collection {get;set;}

    public CollectionModel(ILogger<CollectionModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id is null)
            return Page();

        _logger.LogInformation($"{id}");
        
        var httpClient = _httpClientFactory.CreateClient("CollectionService");
        var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7195");
       
          var tokenRespone = httpClient.RequestClientCredentialsTokenAsync(
           new ClientCredentialsTokenRequest
           {
               Address = disco.TokenEndpoint,
               ClientId = "client_id",
               ClientSecret = "client_secret",
               Scope = "CollectionApi"
           } 
       ).GetAwaiter().GetResult();
       httpClient.SetBearerToken(tokenRespone.AccessToken);
        var response = await httpClient.GetAsync($"collection/collection/{id}");
        if (response.IsSuccessStatusCode)
        {
            Collection = await response.Content.ReadFromJsonAsync<Collection>();
        }
        var collection = await response.Content.ReadAsStringAsync();
        System.Console.WriteLine(collection);


    return Page();

}

    
}

