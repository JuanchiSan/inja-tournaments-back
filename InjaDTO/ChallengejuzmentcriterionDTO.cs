namespace InjaDTO;

public partial class ChallengejuzmentcriterionDTO
{
    public ChallengejuzmentcriterionDTO()
    {
        CriteriaNames = new CriteriaNamesDTO();
    }
    
    public int Id { get; set; }
    
    public int Challengeid { get; set; }

    public int Criteriaid { get; set; }
    
    public string? CriteriaName { get; set; }
    
    public CriteriaNamesDTO CriteriaNames { get; set; }

    public int Divisionid { get; set; }
    
    public string? DivisionName { get; set; }

    public int Rounds { get; set; } = 1;

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
