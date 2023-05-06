using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjaDTO;

public class UsersChallengeCriteriaDTO
{
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

	public int? Divisionid { get; set; }

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

	public PointDTO Point { get; set; }
}
