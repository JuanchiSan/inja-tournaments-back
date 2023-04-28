using System;
using System.Collections.Generic;

namespace InjaDTO;

public partial class JudgmentcriterionDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ChallengejuzmentcriterionDTO> Challengejuzmentcriteria { get; set; } = new List<ChallengejuzmentcriterionDTO>();
}
