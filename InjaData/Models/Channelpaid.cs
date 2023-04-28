using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Channelpaid
{
    public int Id { get; set; }

    public int? Countryid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Country? Country { get; set; }

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
