using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VChallengetypeEventchallengeCriterion
{
    public int? Challengetypeid { get; set; }

    public int? Eventchallengeid { get; set; }

    public int? Challengejudgementid { get; set; }

    public string? Challengetypename { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Criteriaid { get; set; }

    public string? Criterianame { get; set; }

    public string? Criterianamees { get; set; }

    public decimal? Maxscore { get; set; }

    public decimal? Slotstep { get; set; }

    public int? Slotcant { get; set; }

    public int? Hands { get; set; }

    public short? Rounds { get; set; }
}
