using TradesManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TradesManagement.Data.EntityConfig
{
    public class EntityBaseConfig<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("long");

            builder.Property(x => x.CreationDate)
                .HasColumnType("Datetime")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnType("Datetime");
        }
    }
}
