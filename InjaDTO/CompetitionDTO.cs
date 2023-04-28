namespace InjaDTO;

public partial class CompetitionDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Eventid { get; set; }

    public string? Comment { get; set; }

    public short? Wonfirstplace { get; set; }

    public virtual ICollection<ChallengeDTO> Challenges { get; set; } = new List<ChallengeDTO>();

    public virtual ICollection<DivisionDTO> Divisions { get; set; } = new List<DivisionDTO>();

    public virtual EventDTO Event { get; set; } = null!;
}
