using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Point
{
    public int Userid { get; set; }

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

    public int Eventjudgechallengeid { get; set; }

    public decimal? Totalpoints { get; set; }

    public virtual Eventjudgechallengedivision Eventjudgechallenge { get; set; } = null!;

    public virtual Injauser User { get; set; } = null!;
}
