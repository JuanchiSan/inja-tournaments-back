using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Deduction
{
    public int Deductionid { get; set; }

    public int Suptypeid { get; set; }

    public int Supuserid { get; set; }

    public int Supeventid { get; set; }

    public int Ceventid { get; set; }

    public int Cuserid { get; set; }

    public int Ctypeid { get; set; }

    public int Cdivisionid { get; set; }

    public int Ceventchallengeid { get; set; }

    public DateTime? Updatedate { get; set; }

    public DateTime Creationdate { get; set; }

    public string? Comment { get; set; }

    public decimal Score { get; set; }

    public virtual Userinscription C { get; set; } = null!;

    public virtual Injauserusertype Sup { get; set; } = null!;
}
