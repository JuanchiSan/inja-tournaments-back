using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Challengedivision
{
    public int Challengeid { get; set; }

    public int Divisionid { get; set; }

    public int Eventid { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual Division Division { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Inscriptionchallenge> Inscriptionchallenges { get; set; } = new List<Inscriptionchallenge>();
}
