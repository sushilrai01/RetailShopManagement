namespace RetailShopManagement.Domain.Extensions
{
    public static class GuidExtension
    {
        public static Guid ToGuid(this string id)
        {
            var newGuid = Guid.Empty;
            if (Guid.TryParse(id, out var newResult))
            {
                newGuid = newResult;
            }
            return newGuid;
        }

        public static bool Empty(this Guid? id)
        {
            return id == null || id == Guid.Empty;
        }

        public static bool Empty(this Guid id)
        {
            return id == Guid.Empty;
        }
    }
}
