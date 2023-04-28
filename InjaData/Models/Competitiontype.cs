using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Competitiontype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Comment { get; set; }

    public short Active { get; set; }

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();
}
