namespace InjaDTO;

public partial class CountryDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CityDTO> Cities { get; set; } = new List<CityDTO>();
}
