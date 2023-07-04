using System.Text.Json;
using Common.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class TempModel : PageModel
{
    private readonly ILogger<TempModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public IEnumerable<ItemDto> Items {get;set;}

    public TempModel(ILogger<TempModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> OnGet()
{
    var httpClient = _httpClientFactory.CreateClient("CollectionService");

    //var response = await httpClient.GetAsync("items");
    //if (response.IsSuccessStatusCode)
    //{
    //    var jsonString = await response.Content.ReadAsStringAsync();
    //    System.Console.WriteLine(jsonString);
    //     var options = new JsonSerializerOptions
    //    {
    //        PropertyNameCaseInsensitive = true
    //    };
    //    var items = JsonSerializer.Deserialize<IEnumerable<ItemDto>>(jsonString, options);
    //    Items = items;
    //}
    //else{
    //    System.Console.WriteLine("Error");
    //}
    var headers = HttpContext.Request.Headers;
    foreach (var item in headers)
    {
        System.Console.WriteLine(item.Key + " " + item.Value);
    }
    var response = await httpClient.GetAsync("users/GetByName/vast");
    var jsonstring = await response.Content.ReadAsStringAsync();
    _logger.LogInformation(jsonstring + " " +DateTimeOffset.Now);




    return Page();
}
}

