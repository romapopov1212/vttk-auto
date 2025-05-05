using Microsoft.EntityFrameworkCore;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Infractructure.Context;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
    {
        
    }
    public DbSet<CarEntity> Car { get; set; }
    public DbSet<SpecificationsEntity> Specifications { get; set; }
    public DbSet<CarClassificationEntity> CarClassification { get; set; }
    public DbSet<TransportPriceEntity> TransportPrices { get; set; }
}