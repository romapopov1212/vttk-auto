using vttk_auto.Application.Abstractions.Repository;
using vttk_auto.Domain.Entities;
using vttk_auto.Infractructure.Context;

namespace vttk_auto.Infractructure.Repositories;

public class CarRepository(CarDbContext context) : ICarRepository
{
    public async Task<Guid> Add(CarEntity car)
    {
        var newEntity = await context.AddAsync(car);
        await context.SaveChangesAsync();
        return newEntity.Entity.Id;
    }
}