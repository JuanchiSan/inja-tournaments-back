using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VCupNailArt
{
    public int? Eventid { get; set; }

    public string? Divisionname { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendernumber { get; set; }

    public string? Contendername { get; set; }

    public decimal? FinalPoint { get; set; }
}
