using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Eventchallengedivision
{
    public int Eventchallengeid { get; set; }

    public int Divisionid { get; set; }

    public int Minimumcontnders { get; set; }

    public virtual Division Division { get; set; } = null!;

    public virtual Eventchallenge Eventchallenge { get; set; } = null!;

    public virtual ICollection<Eventjudgechallengedivision> Eventjudgechallengedivisions { get; set; } = new List<Eventjudgechallengedivision>();

    public virtual ICollection<Userinscription> Userinscriptions { get; set; } = new List<Userinscription>();
}
