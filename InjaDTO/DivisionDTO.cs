namespace InjaDTO;

public partial class DivisionDTO
{
    public int Id { get; set; }

    public int Competitionid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ChallengejuzmentcriterionDTO> Challengejuzmentcriteria { get; set; } = new List<ChallengejuzmentcriterionDTO>();

    public virtual CompetitionDTO Competition { get; set; } = null!;
}
