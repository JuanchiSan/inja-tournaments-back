using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VWinnerOfWinner
{
    public int? Eventid { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendername { get; set; }

    public string? Contendernumber { get; set; }

    public decimal? Finalpoints { get; set; }

    public long? Rank { get; set; }
}
