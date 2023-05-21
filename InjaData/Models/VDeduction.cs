using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VDeduction
{
    public int? Judgeid { get; set; }

    public string? Judgename { get; set; }

    public decimal? Score { get; set; }

    public string? Comment { get; set; }

    public DateTime? Creationdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public int? Contenderid { get; set; }

    public string? Contendername { get; set; }

    public string? Contendernumber { get; set; }

    public string? Nickname { get; set; }

    public int? Eventid { get; set; }

    public int? Challengeid { get; set; }

    public string? Eventchallengename { get; set; }

    public int? Divisionid { get; set; }

    public string? Divisionname { get; set; }

    public int? Deductionnumber { get; set; }
}
