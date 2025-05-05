namespace vttk_auto.Domain.Entities;

public class TransportPriceEntity
{
    public Guid Id { get; set; }
    public string DepartureCity { get; set; } = String.Empty;
    public string DepartureCitySlug { get; set; } = String.Empty;
    public string DestinationCity { get; set; } = String.Empty;
    public string DestinationCitySlug { get; set; } = String.Empty;
    public decimal KeiCarPrice { get; set; }
    public decimal SedanPrice { get; set; }
    public decimal LongSedanPrice { get; set; }
    public decimal CrossoverPrice { get; set; }
    public decimal JeepPrice { get; set; }
}