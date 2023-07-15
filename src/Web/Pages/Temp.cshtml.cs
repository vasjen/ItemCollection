using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Common.Core.Entities;
using Common.Models;
using Duende.IdentityServer;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class TempModel : PageModel
{
    private readonly ILogger<TempModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public IEnumerable<ItemDto> Items {get;set;}
    public List<Claim> Claims {get;set;}
    public JwtSecurityToken Token {get;set;}

    public TempModel(ILogger<TempModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IActionResult> OnGet()
    {
      
       // var authClient = _httpClientFactory.CreateClient();
       // var disco = await authClient.GetDiscoveryDocumentAsync("http://localhost:10000");
       // var tokenRespone = await authClient.RequestClientCredentialsTokenAsync(
       //     new ClientCredentialsTokenRequest
       //     {
       //         Address = disco.TokenEndpoint,
       //         ClientId = "client_id",
       //         ClientSecret = "client_secret",
       //         Scope = "CollectionApi"
       //     } 
       // );
       // var itemClient = _httpClientFactory.CreateClient();
       // //itemClient.SetBearerToken(tokenRespone.AccessToken);
//
       // var response = await itemClient.GetAsync("http://localhost:5254/Item");
       // System.Console.WriteLine(response.StatusCode);
       // if (!response.IsSuccessStatusCode)
       //     ViewData["Message"] = response.StatusCode;
       // else
       // {
       //     var message = await response.Content.ReadAsStringAsync();
       //     ViewData["Message"] = message;
       // }
        //var jsonToken = await HttpContext.GetTokenAsync("access_token");
        //var token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(jsonToken);
        //Token = token;
        //System.Console.WriteLine(jsonToken);
        //_logger.LogInformation("Access_token"+token);
        //System.Console.WriteLine("\n\n\n\n");
        //var idTokkenJson = await HttpContext.GetTokenAsync("id_token");
        //System.Console.WriteLine(idTokkenJson);
        //var idtoken = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(idTokkenJson);
        //_logger.LogInformation("IdTokken"+idtoken);
        return Page();
    }
  
}

