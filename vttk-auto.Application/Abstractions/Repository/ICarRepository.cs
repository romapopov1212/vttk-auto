using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Abstractions.Repository;

public interface ICarRepository
{
    Task<Guid> Add(CarEntity car);
    Task<CarEntity?> GetById(Guid id);
    Task<List<string>> GetBrands();
    Task<List<string>> GetModels(string brand);
    Task<List<string>> GetModifications(string brand, string model);
}