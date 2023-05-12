using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VCriteriasJudge
{
    public int? Eventchallengeid { get; set; }

    public int? Challengeid { get; set; }

    public int? Eventid { get; set; }

    public string? Eventchallengename { get; set; }

    public short? Rounds { get; set; }

    public int? Criteriaid { get; set; }

    public string? Criterianame { get; set; }

    public string? Divisionsjudges { get; set; }
}
