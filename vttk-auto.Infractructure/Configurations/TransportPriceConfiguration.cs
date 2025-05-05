using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Infractructure.Configurations;

public class TransportPriceConfiguration : IEntityTypeConfiguration<TransportPriceEntity>
{
    public void Configure(EntityTypeBuilder<TransportPriceEntity> builder)
    {
        builder.HasKey(p => p.Id);
    }
}