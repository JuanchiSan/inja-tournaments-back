using InjaData.Models;

namespace InjaDTO;

public partial class CityDTO
{
  public static CityDTO FromDbItem(City dbCity)
  {
    return new CityDTO
    {
      Id = dbCity.Id,
      Name = dbCity.Name,
      Countryid = dbCity.Countryid
    };
  }

  public int Id { get; set; }

  public int Countryid { get; set; }

  public string Name { get; set; } = null!;
}