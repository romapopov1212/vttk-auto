using vttk_auto.Application.Models.Cars;

namespace vttk_auto.Application.Abstractions.Services;

public interface ICarService
{
    Task<Guid> CreateCar(CarToAdd car);
}