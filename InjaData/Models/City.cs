namespace InjaData.Models;

public partial class City
{
	public City()
	{
		Personaddresses = new HashSet<Personaddress>();
	}

	public int Idcity { get; set; }
	public short Idcountry { get; set; }
	public string Cityname { get; set; } = null!;

	public virtual Country IdcountryNavigation { get; set; } = null!;
	public virtual ICollection<Personaddress> Personaddresses { get; set; }
}
