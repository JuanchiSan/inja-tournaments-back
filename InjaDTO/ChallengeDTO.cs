namespace InjaDTO;

public partial class ChallengeDTO
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;
	public DateTime? ChallengeStartDate { get; set; }
	public DateTime? ChallengeEndDate { get; set; }
	
	public int ChallengeId { get; set; }
	public int EventChallengeId { get; set; }
	public string? ChallengeName { get; set; }
	public string? EventChallengeName { get; set; }
	
	public virtual ICollection<DivisionDTO> Divisions { get; set; } = new List<DivisionDTO>();
	
	//public virtual ICollection<ChallengejuzmentcriterionDTO> Challengejuzmentcriteria { get; set; } = new List<ChallengejuzmentcriterionDTO>();

	public virtual ICollection<ChallengeDTO> ChallengeidDependencies { get; set; } = new List<ChallengeDTO>();
}
