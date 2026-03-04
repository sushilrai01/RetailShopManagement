namespace RetailShopManagement.Domain.Shared.Messages;

public static class ReturnMessage
{
    public static string Success(string method, string action) => $" {method} {action} success.";
    public static string CreateSuccess(string method) => $" {method} created successfully.";
    public static string UpdateSuccess(string method) => $" {method} updated successfully.";
    public static string DeleteSuccess(string method) => $" {method} deleted successfully.";
    public static string RetrieveSuccess(string method) => $" {method} fetched successfully.";
    public static string RemoveSuccess(string method) => $" {method} removed successfully.";
    public static string SellSuccess(string method) => $" {method} sold successfully.";
    public static string PaymentSuccess(string method) => $" {method} paid successfully.";
    public static string GenerateSuccess(string method) => $" {method} generated successfully.";
    public static string SavedSuccess(string method) => $" {method} saved successfully.";
    public static string CancelledSuccess(string method) => $" {method} cancelled successfully.";
    public static string SentSuccess(string method) => $" {method} sent successfully.";
    public static string Success(string method) => $" {method} successfully.";

    public static string Failed(string method, string action) => $" {method} {action} fail.";
    public static string CreateFailed(string method) => $" {method} create failed.";
    public static string UpdateFailed(string method) => $" {method} update failed.";
    public static string DeleteFailed(string method) => $" {method} delete failed.";
    public static string RetrieveFailed(string method) => $" {method} fetch failed.";
    public static string RemoveFailed(string method) => $" {method} remove failed.";
    public static string SellFailed(string method) => $" {method} sell failed.";
    public static string PaymentFailed(string method) => $" {method} payment failed.";
    public static string GenerateFailed(string method) => $" {method} generate failed.";
    public static string SaveFailed(string method) => $" {method} save failed.";
    public static string CancelFailed(string method) => $" {method} cancellation failed.";
    public static string SendFailed(string method) => $" {method} send failed.";
    public static string Failed(string method) => $" {method} failed.";


    public static string AuthenticationError() => "Authentication error.";
    public static string JsonError() => "Json cast exception.";
    public static string InternalServerError() => "Something went wrong.";
}