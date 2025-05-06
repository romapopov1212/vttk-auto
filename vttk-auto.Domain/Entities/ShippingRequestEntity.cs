namespace vttk_auto.Domain.Entities;

public class ShippingRequestEntity
{
    public Guid Id { get; set; }
    public string DepartureCity { get; set; } = string.Empty;
    public string ToCity { get; set; } = string.Empty;
    public Guid CarId { get; set; }
    public decimal Price { get; set; }
    public string RouteInfo { get; set; } = string.Empty;
    public string CarInfo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    public CarEntity? Car { get; set; }
}