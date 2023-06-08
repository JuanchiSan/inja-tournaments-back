﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VUserpointGroup
{
    public int? Eventid { get; set; }

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

    public decimal? Totalpoints { get; set; }

    public decimal? Maxscore { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? FinalPoint { get; set; }

    public decimal? Desempate1 { get; set; }

    public long? Desempate10 { get; set; }

    public long? Desempate9 { get; set; }

    public long? Desempate8 { get; set; }

    public long? Desempate7 { get; set; }

    public long? Desempate6 { get; set; }

    public long? CantPhotos { get; set; }
}