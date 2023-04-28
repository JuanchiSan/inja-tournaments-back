using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Eventdivision
{
    public int Id { get; set; }

    public int DivisionId { get; set; }

    public virtual Division Division { get; set; } = null!;

    public virtual Event IdNavigation { get; set; } = null!;
}
