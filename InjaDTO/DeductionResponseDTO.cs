namespace InjaDTO;

public class DeductionResponseDTO
{
  public int contenderId { get; set; }
  public string contenderName { get; set; } = string.Empty;
  public int judgeId { get; set; }
  public string judgeName { get; set; } = string.Empty;
  public int divisionId { get; set; }
  public string divisionName { get; set; } = string.Empty;
  public decimal score { get; set; }
  public string? comment { get; set; }
  public DateTime dateCreated { get; set; }
  public DateTime? dateUpdated { get; set; }
}