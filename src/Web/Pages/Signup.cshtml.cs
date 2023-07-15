using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class SignupModel : PageModel
{
    private readonly ILogger<SignupModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public SignupModel(ILogger<SignupModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost()
    {
        System.Console.WriteLine("Form registred");
        var request = HttpContext.Request.Form;
        var httpClient = _httpClientFactory.CreateClient("CollectionService");
        var user = new Dictionary<string, string>
        {
            { "UserName", request["UserName"] },
            { "Email", request["Email"] },
            { "Password", request["Password"] }
        };
        var content = new FormUrlEncodedContent(user);

        var response =  httpClient.PostAsync("https://localhost:7195/user/create", content).GetAwaiter().GetResult();
        if (response.IsSuccessStatusCode)
        {
            return Redirect("Index");
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewData["ErrorMessage"] = errorMessage;
            return Page();
        }
    }
}

