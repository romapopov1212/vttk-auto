using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Abstractions.Repository;

public interface ICarRepository
{
    Task<Guid> Add(CarEntity car);
    Task<List<CarEntity>> GetAll();
    
    Task<CarEntity> GetById(Guid id);
    
    
}