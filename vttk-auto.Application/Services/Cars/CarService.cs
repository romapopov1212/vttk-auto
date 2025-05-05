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
}