﻿namespace InjaDTO;

public class UserDTO
{
	public int Id { get; set; }

	public string Mail { get; set; } = null!;

	public string Pass { get; set; } = null!;

	public string Lastname { get; set; } = null!;

	public string Firstname { get; set; } = null!;

	public string Docnumber { get; set; } = null!;

	public short Docid { get; set; }

	public string? Street { get; set; }

	public string? Number { get; set; }

	public string? Phone { get; set; }

	public int? Cityid { get; set; }

	public short Enabled { get; set; }

	public virtual ICollection<UsertypeDTO> Types { get; set; } = new List<UsertypeDTO>();

}