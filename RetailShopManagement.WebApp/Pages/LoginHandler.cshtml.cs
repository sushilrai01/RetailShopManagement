using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetailShopManagement.Application.CQRS.Admin.Command;

namespace RetailShopManagement.WebApp.Pages;

[AllowAnonymous]
public class LoginHandlerModel(IMediator mediator) : PageModel
{
    [BindProperty] public string Username     { get; set; } = string.Empty;
    [BindProperty] public string Password  { get; set; } = string.Empty;
    [BindProperty] public string ReturnUrl { get; set; } = "/";

    public IActionResult OnGet()
    {
        // If already authenticated, skip login
        if (User.Identity?.IsAuthenticated == true)
            return Redirect(ReturnUrl);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            return Redirect("/login?error=Please+enter+email+and+password");

        var response = await mediator.Send(new LoginUserCommand
        {
            Username    = Username,
            Password = Password
        });

        if (!response.IsSuccess)
            return Redirect("/login?error=Invalid+email+or+password");

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name,  response.Username),
            new(ClaimTypes.Email, response.Email),
            new(ClaimTypes.Role,  response.Role),
            new("UserId",         response.Id.ToString()),
        };

        var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        // ✅ This runs during a real HTTP request — headers are NOT yet sent
        // Set-Cookie will be written correctly to the browser response
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc   = DateTimeOffset.UtcNow.AddMinutes(30)
            });

        return Redirect(ReturnUrl ?? "/");
    }
}