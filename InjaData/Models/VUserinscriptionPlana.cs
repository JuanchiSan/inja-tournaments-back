using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VUserinscriptionPlana
{
    public int? Userid { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Contendername { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public string? Usernumber { get; set; }

    public string? Nickname { get; set; }

    public int? Usertypeid { get; set; }

    public string? Usertypename { get; set; }

    public DateTime? Inscriptiondate { get; set; }

    public int? Eventchallengeid { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Competitiontypeid { get; set; }

    public string? Competitiontypename { get; set; }

    public int? Challengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Eventid { get; set; }
}
