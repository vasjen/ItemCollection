using System.Text.Json;
using Common.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class CollectionModel : PageModel
{
    private readonly ILogger<CollectionModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public IEnumerable<ItemDto> Items {get;set;}

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
        var response = await httpClient.GetAsync($"collections/{id}");
        if (!response.IsSuccessStatusCode)
        {
            System.Console.WriteLine(response.StatusCode);
        }
        var collection = response.Content.ReadAsStringAsync();
        System.Console.WriteLine(collection);


    return Page();
}
}

