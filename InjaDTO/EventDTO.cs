namespace InjaDTO;

public class EventDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? InscriptionStartDate { get; set; }
    public DateTime? InscriptionEndDate { get; set; }

    public int? UserId { get; set; }
    public string? UserEmail { get; set; }

    public virtual ICollection<CompetitionTypeDTO> CompetitionTypes { get; set; } = new List<CompetitionTypeDTO>();
}
