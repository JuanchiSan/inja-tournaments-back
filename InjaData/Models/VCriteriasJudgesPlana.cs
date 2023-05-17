using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VCriteriasJudgesPlana
{
    public int? Eventchallengeid { get; set; }

    public int? Challengeid { get; set; }

    public int? Eventid { get; set; }

    public string? Eventchallengename { get; set; }

    public short? Rounds { get; set; }

    public int? Challengejudgementcriteriaid { get; set; }

    public int? Criteriaid { get; set; }

    public string? Criterianame { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Userid { get; set; }

    public string? Judgename { get; set; }
}
