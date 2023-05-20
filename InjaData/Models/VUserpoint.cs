using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VUserpoint
{
    public string? VupKey { get; set; }

    public int? Eventjudgechallengedivisionid { get; set; }

    public int? Judgeid { get; set; }

    public string? Judgename { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendername { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Eventid { get; set; }

    public int? Challengeid { get; set; }

    public int? Eventchallengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public decimal? Maxscore { get; set; }

    public short? Rounds { get; set; }

    public int? Criteriaid { get; set; }

    public string? Criterianame { get; set; }

    public decimal? Slot1 { get; set; }

    public decimal? Slot2 { get; set; }

    public decimal? Slot3 { get; set; }

    public decimal? Slot4 { get; set; }

    public decimal? Slot5 { get; set; }

    public decimal? Slot6 { get; set; }

    public decimal? Slot7 { get; set; }

    public decimal? Slot8 { get; set; }

    public decimal? Slot9 { get; set; }

    public decimal? Slot10 { get; set; }

    public decimal? Totalpoints { get; set; }

    public string? Comment { get; set; }

    public int? Slotcant { get; set; }
}
