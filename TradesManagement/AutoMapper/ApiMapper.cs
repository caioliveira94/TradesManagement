using AutoMapper;
using TradesManagement.API.ViewModels;
using TradesManagement.Domain.DTO;

namespace TradesManagement.API.AutoMapper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<TradeDTO, TradeVM>().ReverseMap();
        }
    }
}
