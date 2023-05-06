using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VEventchallengedivision
{
    public string? Ecdkey { get; set; }

    public int? Eventid { get; set; }

    public string? Eventname { get; set; }

    public int? Eventchallengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public DateTime? Eventchallengestartdate { get; set; }

    public DateTime? Eventchallengeenddate { get; set; }

    public string? Challengetypename { get; set; }

    public string? Challengetypecomment { get; set; }

    public bool? Challengetypeactive { get; set; }

    public bool? Challengetypeisforteams { get; set; }

    public int? Minimumcontnders { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public bool? Active { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitionname { get; set; }

    public short? Competitionactive { get; set; }
}
