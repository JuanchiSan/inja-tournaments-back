using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class City
{
    public int Id { get; set; }

    public int Countryid { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Injauser> Injausers { get; set; } = new List<Injauser>();
}
