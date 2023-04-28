using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Eventchallenge
{
    public int Eventid { get; set; }

    public int Challengeid { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
