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
                Type = "success"
            });


        public void ShowError(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Error",
                Content = message,
                Type = "danger"
            });

        public void ShowInfo(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Info",
                Content = message,
                Type = "info"
            });

        public void ShowWarning(string message)
            => OnShow?.Invoke(new ToastOption()
            {
                Title = "Retail Shop Warning",
                Content = message,
                Type = "warning"
            });
    }
    public class ToastOption
    {
        public string Title { get; set; } = "Toast Info";
        public string Content { get; set; }
        public string Type { get; set; }
        public int ToastTimeOut { get; set; } = 5000;
        public Guid Id { get; set; }
    }

}
