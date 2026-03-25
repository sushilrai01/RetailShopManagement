using System.Text.RegularExpressions;

namespace RetailShopManagement.Domain.Extensions
{
    public static class StringExtension
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        public static bool IsValidMobile(this string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return false;

            // Check length
            if (mobile.Length != 10)
                return false;

            // Check only digits
            return mobile.All(char.IsDigit);
        }
    }
}
