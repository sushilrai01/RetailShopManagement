using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

namespace RetailShopManagement.WebApp.Services;

public class RevalidatingAuthenticationStateProvider(
    ILoggerFactory loggerFactory)
    : RevalidatingServerAuthenticationStateProvider(loggerFactory)
{
    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState,
        CancellationToken cancellationToken)
    {
        var user = authenticationState.User;

        if (user.Identity?.IsAuthenticated != true)
            return Task.FromResult(false);

        // Validate essential claims are still present
        var userId = user.FindFirst("UserId")?.Value;
        var email  = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var role  = user.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        var isValid = !string.IsNullOrEmpty(userId) && 
                      !string.IsNullOrEmpty(email) &&
                      !string.IsNullOrEmpty(role);

        return Task.FromResult(isValid);
    }
}