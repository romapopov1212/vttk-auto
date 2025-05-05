using vttk_auto.Application.Models.Cars;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Abstractions.Services;

public interface ICarService
{
    Task<Guid> CreateCar(CarEntity car);
}