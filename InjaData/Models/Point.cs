using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Point
{
    public int Userid { get; set; }

    public int Judgeid { get; set; }

    public int Challengeid { get; set; }

    public int Criteriaid { get; set; }

    public int Divisionid { get; set; }

    public decimal? Slot1 { get; set; }

    public decimal? Slot2 { get; set; }

    public decimal? Slot3 { get; set; }

    public decimal? Slot4 { get; set; }

    public decimal? Slot5 { get; set; }

    public decimal? Slot6 { get; set; }

    public decimal? Slot7 { get; set; }

    public decimal? Slot8 { get; set; }

    public decimal? Slot9 { get; set; }

    public decimal? Slot10 { get; set; }

    public string? Comment { get; set; }

    public virtual Challengejuzmentcriterion Challengejuzmentcriterion { get; set; } = null!;

    public virtual Ninjauser Judge { get; set; } = null!;

    public virtual Ninjauser User { get; set; } = null!;
}
