using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Infractructure.Configurations;

public class SpecificationConfiguration : IEntityTypeConfiguration<SpecificationsEntity>
{
    public void Configure(EntityTypeBuilder<SpecificationsEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(s => s.Car)
            .WithOne(c => c.Specifications);
    }
}