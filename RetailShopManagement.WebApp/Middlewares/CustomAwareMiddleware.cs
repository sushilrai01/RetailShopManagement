using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RetailShopManagement.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace RetailShopManagement.WebApp.Middlewares
{
    public class CustomAwareMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                context.Items["UserName"] = user.Identity.Name;
                context.Items["Role"] = user.FindFirst(ClaimTypes.Role)?.Value;
            }
            //context.Items["UserName"] = userName;
            //context.Items["UserId"] = userId;

            await next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomAwareMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAwareMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAwareMiddleware>();
        }
    }
}
