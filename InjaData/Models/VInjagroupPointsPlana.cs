using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VInjagroupPointsPlana
{
    public int? Eventid { get; set; }

    public string? Eventname { get; set; }

    public int? Groupid { get; set; }

    public string? Groupname { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendernumber { get; set; }

    public string? Contendername { get; set; }

    public int? Eventchallengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public decimal? FinalPoint { get; set; }
}
