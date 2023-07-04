using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class SigninModel : PageModel
{
    private readonly ILogger<SigninModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public SigninModel(ILogger<SigninModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost()
    {
        var request = HttpContext.Request.Form;
        var httpClient = _httpClientFactory.CreateClient("CollectionService");
        var user = new Dictionary<string, string>
        {
            { "UserName", request["UserName"] },
            { "Password", request["Password"] }
        };
        var content = new FormUrlEncodedContent(user);
        var response = await httpClient.PostAsync("users/createbearertoken/login", content);
        _logger.LogInformation(await response.Content.ReadAsStringAsync());
           if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("Token", out var tokenValues))
                {
                    var token = tokenValues.FirstOrDefault();
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                
                }
                    return Redirect("/Temp");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewData["ErrorMessage"] = errorMessage;
                return Page();
            }
    }
}

