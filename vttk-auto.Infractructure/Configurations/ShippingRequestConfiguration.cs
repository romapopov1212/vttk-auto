using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Infractructure.Configurations;

public class ShippingRequestConfiguration : IEntityTypeConfiguration<ShippingRequestEntity>
{
    public void Configure(EntityTypeBuilder<ShippingRequestEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.Car)
            .WithOne(c => c.ShippingRequest);
    }
}