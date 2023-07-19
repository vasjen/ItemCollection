using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public List<SelectListItem> Options { get; set ; }

    public CreateModel(ILogger<CreateModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        Options = Enum.GetValues(typeof(Theme))
                    .Cast<Theme>()
                    .Select(v => 
                    new SelectListItem {
                        Text = v.ToString(),
                        Value = ((int)v).ToString()
        }).ToList(); 
        
    }

   
    public IActionResult OnGet()
    {

      

    return Page();
    }

    public async Task OnPostAsync()
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
        var id = HttpContext.User.Claims.Where(p => p.Type == "sub").Select(p => p.Value).First();
        var request = HttpContext.Request.Form;
        var keys = request.Where(p => p.Key == "fieldType").First().Value.First().Split(',');
        var values = request.Where(p => p.Key == "fieldName").First().Value.First().Split(',');
       // var values = request.Where(p => p.Key == "fieldName");
        var fields = new Dictionary<int,string>();
        for (int i = 0; i < keys.Count(); i++)
        {
           fields.Add(int.Parse(keys[i]),values[i]);        
            //System.Console.WriteLine("Key: {0}, value: {1}", keys.ElementAt(i).Value, values.ElementAt(i).Value);
        }
        System.Console.WriteLine("Keys cout: {0}, keys: {1}", keys.Count(), keys);
        var itemClient = _httpClientFactory.CreateClient(); 
      itemClient.SetBearerToken(tokenRespone.AccessToken);  
     
      var collection = new CreateCollectionDto(request["CollectionName"],request["Description"], Theme.Autographs, Guid.Parse(id), fields);
      System.Console.WriteLine($"Collection: {collection.ApplicationUserId} => {collection.Theme} {collection.Theme}");
        
        JsonSerializerOptions options = new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = true // Опционально, для форматирования JSON с отступами
};
       
        var jsonContent = JsonSerializer.Serialize(collection);
        itemClient.DefaultRequestHeaders.Accept.Clear();
        itemClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var responseCreated = await itemClient.PostAsync("https://localhost:7202/Collection",new StringContent(jsonContent, Encoding.UTF8, "application/json"));
        var result = await responseCreated.Content.ReadAsStringAsync();
        System.Console.WriteLine("\n\n\n"+result+"\n\n\n");
        foreach (var item in request)
        {
            System.Console.WriteLine(item.Key + " - " + item.Value);
            
        }
        
    }
}

