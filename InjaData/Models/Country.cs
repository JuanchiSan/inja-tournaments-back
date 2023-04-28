using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Channelpaid> Channelpaids { get; set; } = new List<Channelpaid>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
