using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Division
{
    public int Id { get; set; }

    public int Competitiontypeid { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Challengejuzmentcriterion> Challengejuzmentcriteria { get; set; } = new List<Challengejuzmentcriterion>();

    public virtual Competitiontype Competitiontype { get; set; } = null!;

    public virtual ICollection<Eventchallengedivision> Eventchallengedivisions { get; set; } = new List<Eventchallengedivision>();
}
