using vttk_auto.Domain.Entities;

namespace vttk_auto.Application.Abstractions.Repository;

public interface ICarRepository
{
    Task<Guid> Add(CarEntity car);
}