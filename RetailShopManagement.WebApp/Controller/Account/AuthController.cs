using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Admin.Command;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.WebApp.Services.AppServices.AuthServices;

namespace RetailShopManagement.WebApp.Controller.Account;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var response = await mediator.Send(new LoginUserCommand()
        {
            Username = model.Username,
            Password = model.Password
        });

        if (!response.IsSuccess) return Unauthorized();

        var user = response;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok(new ApiResponse()
        {
            IsSuccess = true,
            Message = "Signed In Successfully",
        });
    }
}