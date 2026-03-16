namespace RetailShopManagement.WebApp.Services.AppServices.ToastService
{
    public class ToastService
    {
        public event Action<ToastOption>? OnShow;

        public void ShowSuccess(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Success",
                Content = message,
                Type = "success",
                Icon = "fa-solid fa-circle-check fa-lg"
            });


        public void ShowError(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Error",
                Content = message,
                Type = "danger",
                Icon = "fa-solid fa-triangle-exclamation fa-lg"
            });

        public void ShowInfo(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Info",
                Content = message,
                Type = "info",
                Icon = "fa-solid fa-circle-info fa-lg"
            });

        public void ShowWarning(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Warning",
                Content = message,
                Type = "warning",
                Icon = "fa-solid fa-circle-exclamation fa-xl"
            });
    }
    public class ToastOption
    {
        public string Title { get; set; } = "Toast Info";
        public string Content { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; } = "fa-solid fa-info";
        public int ToastTimeOut { get; set; } = 3000;
        public Guid Id { get; set; }
    }

}
