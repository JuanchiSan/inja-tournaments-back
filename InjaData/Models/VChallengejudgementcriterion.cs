using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VChallengejudgementcriterion
{
    public int? Id { get; set; }

    public int? Challengeid { get; set; }

    public int? Divisionid { get; set; }

    public int? Criteriaid { get; set; }

    public string? Criterianame { get; set; }

    public string? Criterianamees { get; set; }

    public decimal? Maxscore { get; set; }

    public decimal? Slotstep { get; set; }

    public int? Slotcant { get; set; }

    public int? Hands { get; set; }

    public short? Rounds { get; set; }
}
