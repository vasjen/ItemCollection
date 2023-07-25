using System.Text.Json;
using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class NewItemModel : PageModel
{
    private readonly ILogger<NewItemModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public Collection Collection {get;set;}
    public Fields Fields {get;set;}

    public NewItemModel(ILogger<NewItemModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }


    [HttpGet("/{id}")]
    public async Task<IActionResult> OnGet(string? id)
    {
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
            System.Console.WriteLine("n\nC O L E C\n\n => \n {0}",await response.Content.ReadAsStringAsync());
        }
        else
        {
            return NotFound();
        }
        response = await httpClient.GetAsync($"Collection/collection/GetFields/{id}");
        System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        if (response.IsSuccessStatusCode)
        {
            Fields = Collection.Fields;
            foreach (var item in Fields.FieldBool)
            {
                System.Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("Int: {0}", Fields.FieldInt.Count);
            foreach (var item in Fields.FieldInt)
            {
                System.Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("Date: {0}", Fields.FieldDate.Count);
            foreach (var item in Fields.FieldDate)
            {
                System.Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("string: {0}", Fields.FieldString.Count);
            foreach (var item in Fields.FieldString)
            {
                System.Console.WriteLine(item.Name);
            }
        }
        else
        {
            return NotFound();
        }

       return Page();

    }
     public async Task<IActionResult> OnPost()
    {
       return Page();

    }

    
}

