using System.Text.Json;
using Common.Core.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class ItemModel : PageModel
{
    private readonly ILogger<ItemModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public Collection Collection {get;set;}

    public ItemModel(ILogger<ItemModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    
    public async Task<IActionResult> OnGet(string id)
    {
       


    return Page();

    }

    
}

