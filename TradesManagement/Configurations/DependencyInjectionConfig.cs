using TradesManagement.ApplicationService;
using TradesManagement.Data.Repositories;
using TradesManagement.Domain.Interfaces.DataAccess;
using TradesManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TradesManagement.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            #region Repository
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            //services.AddScoped(typeof(ITradeRepository), typeof(TradeRepository));
            #endregion

            #region Services
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped(typeof(ITradeService), typeof(TradeService));
            #endregion
        }
    }
}
