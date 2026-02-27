using System;

namespace RetailShopManagement.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }

    public abstract class BaseDerivedEntity<T> : BaseEntity<T>
    {
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}


