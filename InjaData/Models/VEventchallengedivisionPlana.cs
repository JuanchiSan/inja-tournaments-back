﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VEventchallengedivisionPlana
{
    public int? Eventid { get; set; }

    public string? Eventname { get; set; }

    public DateTime? Eventstartdate { get; set; }

    public DateTime? Eventenddate { get; set; }

    public DateTime? Eventstartincriptiondate { get; set; }

    public DateTime? Eventendincriptiondate { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public int? Challengeid { get; set; }

    public int? Eventchallengeid { get; set; }

    public string? Challengename { get; set; }

    public string? Eventchallengename { get; set; }

    public DateTime? Eventchallengestartdate { get; set; }

    public DateTime? Eventchallengeenddate { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }
}
