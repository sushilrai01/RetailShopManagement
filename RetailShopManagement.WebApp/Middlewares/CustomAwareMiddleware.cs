using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RetailShopManagement.WebApp.Middlewares
{
    public class CustomAwareMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                context.Items["UserId"] = user.FindFirst(ClaimTypesConst.UserId)?.Value;
                context.Items["FullName"] = user.FindFirst(ClaimTypesConst.FullName)?.Value;
                context.Items["UserName"] = user.FindFirst(ClaimTypesConst.UserName)?.Value;
                context.Items["Email"] = user.FindFirst(ClaimTypes.Email)?.Value;
                context.Items["Address"] = user.FindFirst(ClaimTypes.StreetAddress)?.Value;
                context.Items["IsActive"] = user.FindFirst(ClaimTypesConst.IsActive)?.Value;
                context.Items["MobileNo"] = user.FindFirst(ClaimTypes.MobilePhone)?.Value;
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
