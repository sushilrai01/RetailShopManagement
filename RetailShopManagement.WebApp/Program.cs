using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.IdentityModel.Tokens;
using RetailShopManagement.Application;
using RetailShopManagement.Infrastructure;
using RetailShopManagement.WebApp.Components;
using RetailShopManagement.WebApp.Extensions;
using RetailShopManagement.WebApp.Middlewares;
using RetailShopManagement.WebApp.Services.AppServices;
using RetailShopManagement.WebApp.Services.AppServices.ToastService;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Configure Serilog
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .CreateLogger();

//builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();

builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration);
builder.Services.AddInfrastructure(configuration);

builder.Services.AddApplicationModule();

builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<JsModalService>();

// Add controllers so ApiController endpoints are available
builder.Services.AddControllers();

//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = configuration["JwtConfig:Issuer"],
//            ValidAudience = configuration["JwtConfig:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]!))
//        };
//    });
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "custom_auth_token";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);

        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();


//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();

//builder.Services.AddScoped<AuthenticationStateProvider,
//    ServerAuthenticationStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseCustomAwareMiddleware();

app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapRazorPages();

app.MapFallback(context =>
{
    var requestedPath = context.Request.Path.Value?.TrimStart('/') ?? "unknown";
    context.Response.Redirect($"/not-found/{requestedPath}");
    return Task.CompletedTask;
});

try
{
    Console.WriteLine("Application version {0} starting up...",
        typeof(Program).Assembly.GetName().Version);

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
    throw;
}
finally
{
    Console.WriteLine("Application Started.");
}