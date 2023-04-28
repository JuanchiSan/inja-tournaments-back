using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Judgmentcriterion
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Namees { get; set; }

    public string? Namefr { get; set; }

    public string? Namepr { get; set; }

    public string? Nameit { get; set; }

    public virtual ICollection<Challengejuzmentcriterion> Challengejuzmentcriteria { get; set; } = new List<Challengejuzmentcriterion>();
}
