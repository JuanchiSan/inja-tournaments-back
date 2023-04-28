using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Ninjagroup
{
    public int Id { get; set; }

    public int Eventid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Persongroup> Persongroups { get; set; } = new List<Persongroup>();
}
