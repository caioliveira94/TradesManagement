using System.ComponentModel;

namespace TradesManagement.Domain.Entities.Enum
{
    public enum Status
    {
        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 0,
    }
}
