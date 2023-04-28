using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Persongroup
{
    public int Userid { get; set; }

    public int Groupid { get; set; }

    public DateTime Added { get; set; }

    public int Enabled { get; set; }

    public virtual Ninjagroup Group { get; set; } = null!;

    public virtual Ninjauser User { get; set; } = null!;
}
