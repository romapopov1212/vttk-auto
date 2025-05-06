namespace vttk_auto.Application.Models.Cars;

public record CarToGetAll(
    Guid Id,
    string Brand,
    string Model,
    string Modification
    );