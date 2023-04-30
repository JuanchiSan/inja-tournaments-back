using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Eventchallengedivision
{
    public int Challengeid { get; set; }

    public int Divisionid { get; set; }

    public int Eventid { get; set; }

    public int Minimumcontnders { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual Division Division { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Userinscription> UserinscriptionEventchallengedivisions { get; set; } = new List<Userinscription>();

    public virtual ICollection<Userinscription> UserinscriptionFinals { get; set; } = new List<Userinscription>();
}
