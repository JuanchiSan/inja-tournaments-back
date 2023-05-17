namespace InjaDTO;

public class ContenderDTO
{
  public int? EventId { get; set; }
  public string? EventName { get; set; }

  public int Id { get; set; }

  public string Mail { get; set; } = null!;

  public string Pass { get; set; } = null!;

  public string Lastname { get; set; } = null!;

  public string Firstname { get; set; } = null!;

  public string Docnumber { get; set; } = null!;

  public short Docid { get; set; }

  public string Usertype { get; set; } = null!;

  public string? Phone { get; set; }

  public bool? Active { get; set; }

  public string? Urlphoto { get; set; }
}