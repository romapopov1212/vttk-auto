using vttk_auto.Application.Models.Cars;
using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Abstractions.Services;

public interface ICarService
{
    Task<Guid> CreateCar(CarEntity car);
    Task<CarToGetById?> GetById(Guid id);
    Task<List<CarToGetAll>?> GetAllCars();
    Task<List<CarToGetBrands>?> GetAllBrands();
    Task<List<CarToGetModels>?> GetAllModels(string brand);
    Task<List<CarToGetModifications>?> GetAllModifications(string brand, string model);
}