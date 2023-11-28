using InjaData.Models;

namespace InjaDTO;

public partial class CountryDTO
{
  public static CountryDTO FromDbItem(Country dbCountry)
  {
    return new CountryDTO
    {
      Id = dbCountry.Id,
      Name = dbCountry.Name
    };
  }

  public int Id { get; set; }

  public string Name { get; set; } = null!;

  public virtual ICollection<CityDTO> Cities { get; set; } = new List<CityDTO>();
}