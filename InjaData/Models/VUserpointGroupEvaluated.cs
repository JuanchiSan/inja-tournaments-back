using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VUserpointGroupEvaluated
{
    public int? Eventid { get; set; }

    public string? Eventname { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public int? Challengeid { get; set; }

    public int? Eventchallengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendername { get; set; }

    public string? Contendernumber { get; set; }

    public long? NotEval { get; set; }

    public long? Eval { get; set; }

    public decimal? Maxscore { get; set; }

    public decimal? Totalpoints { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? FinalPoint { get; set; }

    public decimal? CriterioDificultad { get; set; }

    public long? Criterio100 { get; set; }

    public long? Criterio950 { get; set; }

    public long? Criterio900 { get; set; }

    public long? Criterio850 { get; set; }

    public long? Criterio800 { get; set; }

    public long? Criterio750 { get; set; }

    public long? Criterio700 { get; set; }

    public long? Criterio650 { get; set; }

    public long? Criterio600 { get; set; }

    public long? Criterio550 { get; set; }

    public long? Criterio500 { get; set; }

    public long? Criterio450 { get; set; }

    public long? Criterio400 { get; set; }

    public long? CantPhotos { get; set; }
}
