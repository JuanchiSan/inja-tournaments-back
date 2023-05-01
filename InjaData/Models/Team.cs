using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Eventid { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Teaminscription> Teaminscriptions { get; set; } = new List<Teaminscription>();

    public virtual ICollection<Injauser> Injausers { get; set; } = new List<Injauser>();
}
