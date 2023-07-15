using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class SignoutModel : PageModel
{
    private readonly ILogger<SignoutModel> _logger;

    public SignoutModel(ILogger<SignoutModel> logger)
    {
        _logger = logger;
    }

      public IActionResult OnGet()
    {
        return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }
}

