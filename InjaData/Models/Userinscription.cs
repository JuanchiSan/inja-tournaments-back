using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Userinscription
{
    public int Userid { get; set; }

    public int Challengeid { get; set; }

    public int Divisionid { get; set; }

    public int Eventid { get; set; }

    public short Wonfirstplace { get; set; }

    public DateTime Inscriptiondate { get; set; }

    public int? Finalchallengeid { get; set; }

    public int? Finaldivisionid { get; set; }

    public int? Finaleventid { get; set; }

    public DateTime? Finaldate { get; set; }

    public virtual Eventchallengedivision Eventchallengedivision { get; set; } = null!;

    public virtual Eventchallengedivision? Final { get; set; }

    public virtual Injauser User { get; set; } = null!;
}
