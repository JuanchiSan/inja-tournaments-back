using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Inscriptionchallenge
{
    public int Userid { get; set; }

    public int Challengeid { get; set; }

    public int Divisionid { get; set; }

    public int Eventid { get; set; }

    public short Wonfirstplace { get; set; }

    public DateTime Inscriptiondate { get; set; }

    public virtual Challengedivision Challengedivision { get; set; } = null!;

    public virtual Ninjauser User { get; set; } = null!;
}
