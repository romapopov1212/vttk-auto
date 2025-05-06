using Microsoft.EntityFrameworkCore;
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
    public async Task<CarEntity?> GetById(Guid id)
    {
         return await context.Car.FindAsync(id);
    }

    public async Task<List<CarEntity>?> GetAll()
    {
        var cars = await context.Car
            .Distinct()
            .ToListAsync();
        
        return cars;
    }

    public async Task<List<string>?> GetBrands()
    {
        var brands = await context.Car
            .Select(c => c.Brand)
            .Distinct()
            .ToListAsync();
        
        return brands;
    }

    public async Task<List<string>?> GetModels(string brand)
    {
        var models = await context.Car
            .Where(c => c.Brand == brand).
            Select(c => c.Model)
            .Distinct()
            .ToListAsync();
        return models;
    }

    public async Task<List<string>?> GetModifications(string brand, string model)
    {
        var modifications = await context.Car
            .Where(c => c.Brand == brand && c.Model == model)
            .Select(c => c.Modification)
            .Distinct()
            .ToListAsync();
        return modifications;
    }
}