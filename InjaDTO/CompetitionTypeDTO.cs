namespace InjaDTO;

public class CompetitionTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public short? Wonfirstplace { get; set; }

    public virtual ICollection<ChallengeDTO> Challenges { get; set; } = new List<ChallengeDTO>();
}
