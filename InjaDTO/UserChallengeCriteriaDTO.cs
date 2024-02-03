namespace InjaDTO;

// public class UserChallengeCriteriaDTO
// {
//   public int eventId { get; set; } = 0;
//   public int challengeId { get; set; } = 0;
//
//   public DateTime? challengeStardate { get; set; }
//
//   public DateTime? challengeEnddate { get; set; }
//
//   public int contenderId { get; set; } = 0;
//
//   public string? contenderLastname { get; set; }
//
//   public string? contenderFirstname { get; set; }
//
//   public int divisionId { get; set; } = 0;
//
//   public int? judgeId { get; set; }
//
//   public string? judgeLastname { get; set; }
//
//   public string? judgeFirstname { get; set; }
//
//   public string? photoURL { get; set; }
//
//   public List<CriteriaDTO>? criterias { get; set; }
// }
//
// public class CriteriaDTO
// {
//   public int? eventJudgeChallengeDivisionId { get; set; }
//
//   public string? eventjudgechallengedivisionComment { get; set; }
//
//   public int? criteriaid { get; set; }
//
//   public string? judgementCriteriaName { get;set; }
//
//   public decimal? maxScore { get; set; }
//
//   public decimal? slotStep { get; set; }
//
//   public int? slotCant { get; set; }
//
//   public string? scoreByDivision { get; set; }
//
//   public int? hands { get; set; }
//
//   public short? rounds { get; set; }
//
//   public PointDTO? point { get; set; }
// }

public class PhotoUserChallengeCriteriaDTO
{
  public string? PhotoURL { get; set; }
  public List<UserChallengeCriteriaDTO> UserChallengeCriteria { get; set; } = new List<UserChallengeCriteriaDTO>();
}

public class UserChallengeCriteriaDTO
{
  public UserChallengeCriteriaDTO()
  {
    JugdementCriteriaNames = new CriteriaNamesDTO();
  }
  
  public int? eventId { get; set; }

  public int? challengeId { get; set; }

  public DateTime? ChallengeStardate { get; set; }

  public DateTime? ChallengeEnddate { get; set; }

  public string? ChallengeName { get; set; }

  public int? ChallengeMinimumcontenders { get; set; }

  public int? eventJudgeChallengeDivisionId { get; set; }

  public string? EventjudgechallengedivisionComment { get; set; }

  public DateTime? EventjudgechallengedivisionCreationdate { get; set; }

  public DateTime? EventjudgechallengedivisionUpdatedate { get; set; }

  public int? Criteriaid { get; set; }

  public string? Judgementcriterianame { get; set; }
  
  public CriteriaNamesDTO JugdementCriteriaNames { get; set; }

  public int? Divisionid { get; set; }
  
  public string? DivisionName { get; set; }

  public decimal? Maxscore { get; set; }

  public decimal? Slotstep { get; set; }

  public int? Slotcant { get; set; }

  public string? Scorebydivision { get; set; }

  public int? Hands { get; set; }

  public short? Rounds { get; set; }

  public int? Utypeid { get; set; }

  public int? contenderId { get; set; }

  public int? Ueventid { get; set; }

  public string? ContenderLastname { get; set; }

  public string? ContenderFirstname { get; set; }

  public int? Judgeid { get; set; }

  public string? JudgeLastname { get; set; }

  public string? JudgeFirstname { get; set; }

  public PointDTO? Point { get; set; }
}
