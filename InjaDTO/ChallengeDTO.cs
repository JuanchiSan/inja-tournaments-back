namespace InjaDTO;

public partial class ChallengeDTO
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public int Competitionid { get; set; }

	public string? Comment { get; set; }

	public virtual ICollection<ChallengejuzmentcriterionDTO> Challengejuzmentcriteria { get; set; } = new List<ChallengejuzmentcriterionDTO>();

	public virtual CompetitionDTO Competition { get; set; } = null!;

	public virtual ICollection<ChallengeDTO> ChallengeidDependencies { get; set; } = new List<ChallengeDTO>();

	public virtual ICollection<ChallengeDTO> Challenges { get; set; } = new List<ChallengeDTO>();

	public virtual ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
}
