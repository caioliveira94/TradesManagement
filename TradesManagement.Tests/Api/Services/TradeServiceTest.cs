using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using TradesManagement.Domain.Interfaces.Services;
using TradesManagement.ApplicationService;
using TradesManagement.Domain.DTO;

namespace TradesManagement.Tests.Api.Controller
{
    public class TradeServiceTest
    {
        private readonly ITradeService tradeService;

        public TradeServiceTest()
        {
            tradeService = new TradeService();
        }

        [Fact]
        public async Task ProcessCategories()
        {
            var input = new List<TradeDTO>
           {
               new TradeDTO{ Value = 900000, ClientSector = "Private"},
               new TradeDTO{ Value = 1100000, ClientSector = "Private"},
               new TradeDTO{ Value = 1010000, ClientSector = "Public"},
               new TradeDTO{ Value = 900000, ClientSector = "Public"}
           };

            var categoriesReturned = await tradeService.ProcessCategories(input);
            var categoriesExpected = new List<string> { "UNCATEGORIZED", "HIGHRISK", "MEDIUMRISK", "LOWRISK" };

            Assert.Equal(categoriesExpected, categoriesReturned);
        }
    }
}
