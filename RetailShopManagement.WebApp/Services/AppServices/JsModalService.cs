using Microsoft.JSInterop;

namespace RetailShopManagement.WebApp.Services.AppServices;

public class JsModalService(IJSRuntime javascript)
{
    public async Task AlertAsync(string message)
    {
        await javascript.InvokeVoidAsync("alert", message);
    }

    public async Task<bool> ConfirmAsync(string message)
    {
        return await javascript.InvokeAsync<bool>("confirm", message);
    }

    public async Task<string> PromptAsync(string message, string defaultValue)
    {
        return await javascript.InvokeAsync<string>("prompt", message, defaultValue);
    }
    public async Task<string> ConsoleLogAsync(string message)
    {
        return await javascript.InvokeAsync<string>("console.log", message);
    }
}