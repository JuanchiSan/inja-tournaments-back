﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Doctype
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Ninjauser> Ninjausers { get; set; } = new List<Ninjauser>();
}
