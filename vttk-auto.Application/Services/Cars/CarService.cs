using vttk_auto.Application.Abstractions.Repository;
using vttk_auto.Application.Abstractions.Services;
using vttk_auto.Application.Models.Cars;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Services.Cars;

public class CarService(ICarRepository carRepository, ParserCar parser) : ICarService
{
    public async Task<Guid> CreateCar(CarToAdd car)
    {
        var entity = new CarEntity
        {
            Id = Guid.NewGuid(),
            Brand = car.Brand,
            Model = car.Model,
            Modification = car.Modification,
        };
        
        return await carRepository.Add(entity);
    }
}