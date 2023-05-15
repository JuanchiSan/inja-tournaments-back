namespace InjaDTO;

public class ResponseDivisionContenderChallengeDTO
{
  public int EventId { get; set; }
  public string EventName { get; set; } = string.Empty;
  public int DivisionId { get; set; }
  public string DivisionName { get; set; } = string.Empty;
  public int ChallengeId { get; set; }
  public string ChallengeName { get; set; } = string.Empty;
  public int ContenderId { get; set; }
  public string ContenderName { get; set; } = string.Empty;
  public string PhotoURL { get; set; } = string.Empty;
  public int TotalContendersThisChallenge { get; set; }
  public int TotalPhotosInThisChallenge { get; set; }
}