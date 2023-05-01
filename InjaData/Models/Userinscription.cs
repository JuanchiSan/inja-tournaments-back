using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Userinscription
{
    public int Eventchallengeid { get; set; }

    public int Divisionid { get; set; }

    public short Wonfirstplace { get; set; }

    public DateTime Inscriptiondate { get; set; }

    public int Utypeid { get; set; }

    public int Uuserid { get; set; }

    public int Ueventid { get; set; }

    public virtual Eventchallengedivision Eventchallengedivision { get; set; } = null!;

    public virtual Injauserusertype U { get; set; } = null!;
}
