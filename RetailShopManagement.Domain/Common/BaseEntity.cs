using System;

namespace RetailShopManagement.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public abstract class BaseDerivedEntity<T> : BaseEntity<T>
    {
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}


