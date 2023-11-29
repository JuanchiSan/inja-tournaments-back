using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class EventCup
{
    public int EventId { get; set; }

    public string Cup { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
