
namespace TradesManagement.Domain.Entities
{
    public class Trade : EntityBase
    {
        public double Value { get; set; }
        public string ClientSector { get; set; }
        public string TradeCategory { get; set; }
    }
}
