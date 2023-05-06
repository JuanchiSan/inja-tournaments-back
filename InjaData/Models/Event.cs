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

    public virtual Injauser? Creatoruser { get; set; }

    public virtual ICollection<Eventchallenge> Eventchallenges { get; set; } = new List<Eventchallenge>();

    public virtual ICollection<Injagroup> Injagroups { get; set; } = new List<Injagroup>();

    public virtual ICollection<Injauserusertype> Injauserusertypes { get; set; } = new List<Injauserusertype>();

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
