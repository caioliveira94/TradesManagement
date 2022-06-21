using TradesManagement.Domain.Interfaces.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using TradesManagement.Domain.DTO;

namespace TradesManagement.ApplicationService
{
    public class TradeService : ITradeService
    {
        public async Task<List<string>> ProcessCategories(IEnumerable<TradeDTO> tradesList)
        {
            var tradeCategories = new List<string>();
            
            foreach (var trade in tradesList)
                tradeCategories.Add(await GetTradeCategory(trade));

            return tradeCategories;
        }

        private async Task<string> GetTradeCategory(TradeDTO trade)
        {
            if (trade.Value > 1000000)
                return trade.ClientSector == "Private" ? "HIGHRISK" : "MEDIUMRISK";
            else
                return trade.ClientSector == "Private" || 
                    (trade.ClientSector == "Public" && trade.Value == 1000000) ? "UNCATEGORIZED" : "LOWRISK";
        }
    }
}
