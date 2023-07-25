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
    private string identityUrl;

    public CreateModel(ILogger<CreateModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
        
        identityUrl = configuration.GetValue<string>("Identity");
    }

   
    public IActionResult OnGet()
    {

      

    return Page();
    }

    public async Task OnPostAsync()
    {
         var authClient = _httpClientFactory.CreateClient("CollectionService");
       var disco = await authClient.GetDiscoveryDocumentAsync(identityUrl);
       
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
        var keys = request["fieldName"].ToString().Split(',');
        var values = request["fieldType"].ToString().Split(',');
        var fields = new Dictionary<string,int>();
        for (int i = 0; i < keys.Count(); i++)
        {
          fields.Add(keys[i],int.Parse(values[i]));        
        }
        var itemClient = _httpClientFactory.CreateClient("CollectionService"); 
      itemClient.SetBearerToken(tokenRespone.AccessToken);  
     
      var collection = new CreateCollectionDto(request["CollectionName"],request["Description"], Theme.Autographs, Guid.Parse(id), fields);
      System.Console.WriteLine($"Collection: {collection.ApplicationUserId} => {collection.Theme} {collection.Theme}");
        
        JsonSerializerOptions options = new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = true 
};
       
       var jsonContent = JsonSerializer.Serialize(collection);
       itemClient.DefaultRequestHeaders.Accept.Clear();
       itemClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
       var responseCreated = await itemClient.PostAsync("collection",new StringContent(jsonContent, Encoding.UTF8, "application/json"));
       var result = await responseCreated.Content.ReadAsStringAsync();
       System.Console.WriteLine("\n\n\n"+result+"\n\n\n");
       
       if (responseCreated.IsSuccessStatusCode)
        RedirectToPage($"Collection/{result}");
       
    }
}

