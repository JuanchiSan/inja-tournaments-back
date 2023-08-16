using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VInjagroupResult
{
    public int? Eventid { get; set; }

    public string? Eventname { get; set; }

    public int? Groupid { get; set; }

    public string? Groupname { get; set; }

    public decimal? FinalPoint { get; set; }
}
