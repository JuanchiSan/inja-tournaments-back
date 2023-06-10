using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Persongroup
{
    public int Userid { get; set; }

    public int Groupid { get; set; }

    public DateTime Added { get; set; }

    public bool? Enabled { get; set; }

    public virtual Injagroup Group { get; set; } = null!;

    public virtual Injauser User { get; set; } = null!;
}
