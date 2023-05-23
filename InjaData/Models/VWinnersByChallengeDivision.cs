using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VWinnersByChallengeDivision
{
    public int? Eventchallengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendername { get; set; }

    public string? Contendernumber { get; set; }

    public decimal? Totalpoints { get; set; }

    public long? Rank { get; set; }
}
