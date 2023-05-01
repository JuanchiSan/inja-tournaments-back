using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Usertype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Injauserusertype> Injauserusertypes { get; set; } = new List<Injauserusertype>();
}
