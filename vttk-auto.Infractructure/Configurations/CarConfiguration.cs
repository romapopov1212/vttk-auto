using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Infractructure.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Specifications)
            .WithOne(s => s.Car)
            .HasForeignKey<CarEntity>(c => c.SpecificationId)
            .HasPrincipalKey<SpecificationsEntity>(s => s.Id);
    }
}