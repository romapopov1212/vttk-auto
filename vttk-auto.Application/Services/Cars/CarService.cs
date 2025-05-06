using Microsoft.EntityFrameworkCore;
using vttk_auto.Application.Abstractions.Repository;
using vttk_auto.Application.Abstractions.Services;
using vttk_auto.Application.Models.Cars;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Services.Cars;

public class CarService(ICarRepository carRepository) : ICarService
{
    public async Task<Guid> CreateCar(CarEntity car)
    {
        return await carRepository.Add(car);
    }

    public async Task<CarToGetById?> GetById(Guid id)
    {
        var entity = await carRepository.GetById(id);
        if (entity == null) return null;
        
        var car = new CarToGetById(entity.Id, entity.Brand, entity.Model, entity.Modification);
        
        return car;
    }

    public async Task<List<CarToGetAll>?> GetAllCars()
    {
        var entities = await carRepository.GetAll();
        if (entities is null) return null;
        var cars = entities.Select(c => new CarToGetAll(c.Id, c.Brand, c.Model, c.Modification)).ToList();
        
        return cars;
    }

    public async Task<List<CarToGetBrands>?> GetAllBrands()
    {
        var strBrands = await carRepository.GetBrands();
        if (strBrands is null) return null;
        
        var brands = strBrands.Select(b => new CarToGetBrands(b)).ToList();
        return brands;
    }

    public async Task<List<CarToGetModels>?> GetAllModels(string brand)
    {
        var strModels = await carRepository.GetModels(brand);
        if (strModels is null) return null;
        
        var models = strModels.Select(m => new CarToGetModels(m)).ToList();
        return models;
    }

    public async Task<List<CarToGetModifications>?> GetAllModifications(string brand, string model)
    {
        var strModifications = await carRepository.GetModifications(brand, model);
        if (strModifications is null) return null;
        
        var models = strModifications.Select(m => new CarToGetModifications(m)).ToList();
        return models;
    }
}