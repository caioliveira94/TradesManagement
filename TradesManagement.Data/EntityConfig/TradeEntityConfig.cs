using TradesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TradesManagement.Data.EntityConfig
{
    public class TradeEntityConfig : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            //builder.HasKey(x => x.Id);

            


        }
    }
}
