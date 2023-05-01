using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Eventchallenge
{
    public int Eventid { get; set; }

    public int Challengeid { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Eventchallengedivision> Eventchallengedivisions { get; set; } = new List<Eventchallengedivision>();
}
