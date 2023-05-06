using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VPoint
{
    public int? Eventid { get; set; }

    public int? Divisionid { get; set; }

    public int? Challengeid { get; set; }

    public string? Name { get; set; }

    public string? Judgelastname { get; set; }

    public string? Judgefirstname { get; set; }

    public int? Userid { get; set; }

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

    public string? Comment { get; set; }

    public int? Eventjudgechallengeid { get; set; }
}
