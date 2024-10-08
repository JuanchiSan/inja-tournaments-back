﻿namespace InjaDTO;

public class UserParticipationDTO
{
  public int ContenderId { get; set; }
  public string Name { get; set; } = string.Empty;
  public int? ContenderNumber { get; set; }
  public List<UserEventDTO>? Events { get; set; } = new();
}

public class UserEventDTO : IEqualityComparer<UserEventDTO>
{
  public int EventId { get; set; }
  public string EventName { get; set; } = string.Empty;
  public bool PointsPublished { get; set; }
  public DateTime? PointsPublishedDate { get; set; }
  public string? PointsPublishedUserAuth { get; set; }
  
  public string? NotPublishedPointsMessage { get; set; }
  
  public List<UserCompetitionsDTO>? UserCompetitions { get; set; } = new();

  public List<UserCupsDTO> UserCups { get; set; } = new();

  public List<UserPointGroupDTO> UserGroups { get; set; } = new();

  public bool Equals(UserEventDTO? x, UserEventDTO? y)
  {
    return x != null && y != null && x.EventId == y.EventId;
  }

  public int GetHashCode(UserEventDTO obj)
  {
    return 1;
  }
}

public class UserPointGroupDTO
{
  public string GroupName { get; set; } = string.Empty;
  //public string Participants { get; set; }
  public decimal TotalPoints { get; set; }

  public string EventChallengeName { get; set; } = string.Empty;
  public int GroupPosition { get; set; }
}

public class UserCupsDTO
{
  public string CupName { get; set; }
  public int Position { get; set; }
  
  public string DivisionName { get; set; } = string.Empty;
  public decimal TotalPoints { get; set; }
}

public class UserCompetitionsDTO : IEquatable<UserCompetitionsDTO>
{
  public int CompetitionId { get; set; }
  public string CompetitionName { get; set; } = string.Empty;
  public List<UserEventChallengesDTO>? Challenges { get; set; } = new();
  public bool Equals(UserCompetitionsDTO? other)
  {
    return other != null && other.CompetitionId == CompetitionId;
  }
}

public class UserEventChallengesDTO
{
  public int ChallengeId { get; set; }
  public int EventChallengeId { get; set; }
  public string EventChallengeName { get; set; } = string.Empty;

  public int MaxRoundCant { get; set; } = 0;
  public List<UserEventChallengeDivisionDTO>? Division { get; set; } = new();
}

public class UserEventChallengeDivisionDTO
{
  public int DivisionId { get; set; }
  public string DivisionName { get; set; } = string.Empty;

  public decimal MaxPoints { get; set; }
  public decimal? UserPoints { get; set; }
  
  public int? UserPosition { get; set; }
  public string? PhotoUrl { get; set; }
  
  public List<UserEventChallengeCriteriasDTO> Points { get; set; } = new();
  public List<UserPointsDeductionsDTO> Deductions { get; set; } = new();
}

public class UserEventChallengeCriteriasDTO
{
  public UserEventChallengeCriteriasDTO()
  {
    CriteriaNames = new CriteriaNamesDTO();
  }
  
  public int CriteriaId { get; set; }
  public string CriteriaName { get; set; } = string.Empty;
  public CriteriaNamesDTO CriteriaNames { get; set; }

  public short? Round { get; set; } = 1;

  public decimal? UserPoints { get; set; }
  public decimal MaxPoints { get; set; }
  public int CantSlots { get; set; }
  public int? JudgeId { get; set; }
  public string? JudgeName { get; set; }
  public List<UserPointsDTO>? Points { get; set; } = new();
}

public class UserPointsDTO
{
  public int SlotId { get; set; }
  public decimal? Points { get; set; } 
}

public class UserPointsDeductionsDTO
{
  public int DeductionId { get; set; }
  public decimal Points { get; set; }
  public string Comment { get; set; } = string.Empty;
  public int JudgeId { get; set; }
  public string JudgeName { get; set; } = string.Empty;
}