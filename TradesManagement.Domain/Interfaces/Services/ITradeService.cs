using System.Collections.Generic;
using System.Threading.Tasks;
using TradesManagement.Domain.DTO;

namespace TradesManagement.Domain.Interfaces.Services
{
    public interface ITradeService //: IBaseService<Trade, long>
    {
        Task<List<string>> ProcessCategories(IEnumerable<TradeDTO> tradesList);
    }
}
