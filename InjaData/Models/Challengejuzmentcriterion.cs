﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Challengejuzmentcriterion
{
    public int Challengeid { get; set; }

    public int Criteriaid { get; set; }

    public int? Divisionid { get; set; }

    public decimal Maxscore { get; set; }

    public int Slotcant { get; set; }

    public decimal Slotstep { get; set; }

    public string? Scorebydivision { get; set; }

    public int Hands { get; set; }

    public int Id { get; set; }

    public short? Rounds { get; set; }

    public virtual Challengetype Challenge { get; set; } = null!;

    public virtual Judgmentcriterion Criteria { get; set; } = null!;

    public virtual Division? Division { get; set; }

    public virtual ICollection<Eventjudgechallengedivision> Eventjudgechallengedivisions { get; set; } = new List<Eventjudgechallengedivision>();
}
