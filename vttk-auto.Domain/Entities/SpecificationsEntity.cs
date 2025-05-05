namespace vttk_auto.Domain.Entities;

public class SpecificationsEntity
{
    public Guid Id { get; set; }
    public string Height { get; set; } = string.Empty;
    public string Width { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    
    public string Length { get; set; } = string.Empty;
    
    public Guid CarId { get; set; }
    public CarEntity? Car { get; set; }
}