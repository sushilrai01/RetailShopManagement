using RetailShopManagement.Application;
using RetailShopManagement.Infrastructure;
using RetailShopManagement.WebApp.Components;
using RetailShopManagement.WebApp.Extensions;
using RetailShopManagement.WebApp.Services.AppServices;
using System.Reflection;
using RetailShopManagement.WebApp.Services.AppServices.ToastService;

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

builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration); 
builder.Services.AddInfrastructure(configuration);

builder.Services.AddApplicationModule();

builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<JsModalService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

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