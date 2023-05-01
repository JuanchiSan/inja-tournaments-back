using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Teaminscription
{
    public int Eventchallengeid { get; set; }

    public int Divisionid { get; set; }

    public DateTime Inscriptiondate { get; set; }

    public int Teamid { get; set; }

    public virtual Team Team { get; set; } = null!;
}
