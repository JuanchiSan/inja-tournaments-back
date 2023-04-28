using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Inscription
{
    public long Id { get; set; }

    public int Userid { get; set; }

    public int Eventid { get; set; }

    public DateTime Date { get; set; }

    public short? Paid { get; set; }

    public DateTime? Paiddate { get; set; }

    public decimal? Paidamount { get; set; }

    public int? Channelid { get; set; }

    public virtual Channelpaid? Channel { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Ninjauser User { get; set; } = null!;
}
