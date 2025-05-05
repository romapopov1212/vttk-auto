namespace vttk_auto.Domain.Entities;

public class CarClassificationEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MixLength {get; set;}
    public int MaxLength {get; set;}
    public int MinHeight {get; set;}
    public int MaxHeight {get; set;}
}