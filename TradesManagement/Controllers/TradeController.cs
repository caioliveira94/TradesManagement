using AutoMapper;
using TradesManagement.API.ViewModels;
using TradesManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradesManagement.Domain.DTO;

namespace TradesManagement.API.Controllers
{
    //[Authorize]
    public class TradeController : BaseController
    {
        private readonly ITradeService tradeService;
        private readonly IMapper mapper;

        public TradeController(ITradeService tradeService, IMapper mapper)
        {
            this.tradeService = tradeService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IEnumerable<TradeVM> tradeModel)
        {
            if (IsArgumentNull(tradeModel))
                return CustomResponse();

            try
            {
                var trades = this.mapper.Map<IEnumerable<TradeDTO>>(tradeModel);
                var categories = await this.tradeService.ProcessCategories(trades);

                return CustomResponsePreserve(categories);
            }
            catch (Exception e)
            {
                AddError("error: " + e.Message);
                return CustomResponse();
            }
        }
    }
}
