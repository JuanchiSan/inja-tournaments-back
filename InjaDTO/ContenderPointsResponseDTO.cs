namespace InjaDTO;

public class ContenderPointsResponseDTO
{
  public int JudgeId { get; set; }
  public string JudgeName { get; set; } = string.Empty;
  public int ContenderId { get; set; }
  public string ContenderName { get; set; } = string.Empty;
  public int CriteriaId { get; set; }
  public string CriteriaName { get; set; } = string.Empty;

  public int ChallengeId { get; set; }
  public string ChallengeName { get; set; } = string.Empty;
  public int EventJugdeChallengeDivisionId { get; set; }
  
  public int DivisionId { get; set; }
  public string DivisionName { get; set; } = string.Empty;
  
  public int CompetitionId { get; set; }
  public string CompetitinoName { get; set; } = string.Empty;
  public int Round { get; set; }
  public int SlotCant { get; set; }
  
  public List<decimal?> Points { get; set;  }= new(); 
  
  public decimal TotalPoints { get; set; }
  public decimal MaxPoints { get; set; }
  public string Comment { get; set; } = string.Empty;

  public string PhotoURL { get; set; } = string.Empty;
  
  public int Hands { get; set; }
  public int SlotStep { get; set; }
}

