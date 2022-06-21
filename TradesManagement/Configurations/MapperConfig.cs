using TradesManagement.API.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TradesManagement.API.Configurations
{
    public static class MapperConfig
    {
        public static IServiceCollection UseApiMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(ApiMapper));
            return service;
        }
    }
}
