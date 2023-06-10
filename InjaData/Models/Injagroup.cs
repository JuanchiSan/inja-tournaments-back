using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Injagroup
{
    public string Name { get; set; } = null!;

    public int Eventid { get; set; }

    public bool Isstudent { get; set; }

    public int Id { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Persongroup> Persongroups { get; set; } = new List<Persongroup>();
}
