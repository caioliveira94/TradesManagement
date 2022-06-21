using System;

namespace TradesManagement.Domain.Entities
{
    public class EntityBase
    {
        public long Id { get; set; }
        private DateTime? CreatedAt;
        public DateTime? CreationDate
        {
            get { return CreatedAt; }
            set { CreatedAt = (value == null ? DateTime.Now : value); }
        }
        public DateTime LastUpdate { get; set; }
    }
}
