using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Creatoruserid { get; set; }

    public string? Comment { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public DateTime Startinscription { get; set; }

    public DateTime Endinscription { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Challengedivision> Challengedivisions { get; set; } = new List<Challengedivision>();

    public virtual Ninjauser? Creatoruser { get; set; }

    public virtual ICollection<Eventchallenge> Eventchallenges { get; set; } = new List<Eventchallenge>();

    public virtual Eventdivision? Eventdivision { get; set; }

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    public virtual ICollection<Ninjagroup> Ninjagroups { get; set; } = new List<Ninjagroup>();
}
