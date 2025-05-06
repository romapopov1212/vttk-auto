namespace vttk_auto.Domain.Entities;

public class CarEntity
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Modification { get; set; } = string.Empty;
    public Guid SpecificationId { get; set; }
    public SpecificationsEntity? Specifications { get; set; }
    
    public ShippingRequestEntity? ShippingRequest { get; set; }
}