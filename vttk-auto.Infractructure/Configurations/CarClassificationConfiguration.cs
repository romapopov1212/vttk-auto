using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Infractructure.Configurations;

public class CarClassificationConfiguration : IEntityTypeConfiguration<CarClassificationEntity>
{
    public void Configure(EntityTypeBuilder<CarClassificationEntity> builder)
    {
        builder.HasKey(c => c.Id);
    }
}