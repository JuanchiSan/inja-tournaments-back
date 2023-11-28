using InjaData.Models;

namespace InjaDTO;

public class UserDTO
{
	public static UserDTO FromVInjaUser(VInjauser dbItem)
	{
		return new UserDTO
		{
			Id = (int)dbItem.Injauserid!,
			Lastname = dbItem.Lastname ?? string.Empty,
			Firstname = dbItem.Firstname ?? string.Empty,
			Phone = dbItem.Phone,
			Mail = dbItem.Mail ?? string.Empty,
			User_Number = dbItem.UserNumber,
			Docid = (short)dbItem.Docid!,
			Docnumber = dbItem.Docnumber ?? string.Empty
		};
	}
	public int Id { get; set; }

	public string Mail { get; set; } = null!;

	public string Pass { get; set; } = null!;

	public string Lastname { get; set; } = null!;

	public string Firstname { get; set; } = null!;

	public string Docnumber { get; set; } = null!;

	public string Name => $"{Lastname}, {Firstname}";
	
	public short Docid { get; set; }

	public string? Street { get; set; }

	public string? User_Number { get; set; }

	public string? Phone { get; set; }

	public int? Cityid { get; set; }
	
	public string? CityName { get; set; }
	
	public int? CountryId { get; set; }
	
	public string? CountryName { get; set; }

	public short Enabled { get; set; }
}
