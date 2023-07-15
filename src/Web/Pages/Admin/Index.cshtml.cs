using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Personal;

public class IndexModel : PageModel
{
    private readonly ILogger<SignupModel> _logger;

    public IndexModel(ILogger<SignupModel> logger)
    {
        _logger = logger;
    }
    
    public void OnGet()
    {
    }
}

