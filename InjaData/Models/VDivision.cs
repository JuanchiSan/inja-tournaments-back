using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VDivision
{
    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public bool? Divisionactive { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public short? Competitiontypeactive { get; set; }

    public string? Competitiontypecomment { get; set; }
}
