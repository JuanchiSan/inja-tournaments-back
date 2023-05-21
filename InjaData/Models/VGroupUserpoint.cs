﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VGroupUserpoint
{
    public int? Eventid { get; set; }

    public int? Eventchallengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendername { get; set; }

    public long? NotEval { get; set; }

    public long? Eval { get; set; }

    public decimal? Totalpoints { get; set; }

    public decimal? Maxscore { get; set; }
}