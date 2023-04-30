using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Doctype
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Injauser> Injausers { get; set; } = new List<Injauser>();
}
