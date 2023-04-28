namespace InjaDTO;

public partial class ChallengejuzmentcriterionDTO
{
    public int Challengeid { get; set; }

    public int Criteriaid { get; set; }

    public int Divisionid { get; set; }

    public decimal Maxscore { get; set; }

    public int Slotcant { get; set; }

    public string? Scorebydivision { get; set; }

    public decimal? SlotStep { get; set; }

    public int? Hands { get; set; }

    public virtual ChallengeDTO Challenge { get; set; } = null!;

    public virtual JudgmentcriterionDTO Criteria { get; set; } = null!;

    public virtual DivisionDTO Division { get; set; } = null!;

    public virtual ICollection<PointDTO> Points { get; set; } = new List<PointDTO>();
}
