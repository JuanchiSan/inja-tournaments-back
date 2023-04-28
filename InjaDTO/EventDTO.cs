namespace InjaDTO;

public partial class EventDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Creatoruserid { get; set; }

    public string? Comment { get; set; }

    public virtual ICollection<CompetitionDTO> Competitions { get; set; } = new List<CompetitionDTO>();

    public virtual UserDTO? Creatoruser { get; set; }

    public virtual ICollection<GroupDTO> Groups { get; set; } = new List<GroupDTO>();

    public virtual ICollection<InscriptionDTO> Inscriptions { get; set; } = new List<InscriptionDTO>();
}
