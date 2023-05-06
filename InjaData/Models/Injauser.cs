using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Injauser
{
    public int Id { get; set; }

    public string Mail { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Docnumber { get; set; } = null!;

    public short Docid { get; set; }

    public string Usertype { get; set; } = null!;

    public string? Street { get; set; }

    public string? Number { get; set; }

    public string? Phone { get; set; }

    public int? Cityid { get; set; }

    public bool? Active { get; set; }

    public string? Urlphoto { get; set; }

    public virtual City? City { get; set; }

    public virtual Doctype Doc { get; set; } = null!;

    public virtual ICollection<Eventjudgechallengedivision> Eventjudgechallengedivisions { get; set; } = new List<Eventjudgechallengedivision>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Injauserusertype> Injauserusertypes { get; set; } = new List<Injauserusertype>();

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    public virtual ICollection<Persongroup> Persongroups { get; set; } = new List<Persongroup>();

    public virtual ICollection<Photo> PhotoContenders { get; set; } = new List<Photo>();

    public virtual ICollection<Photo> PhotoPhotographers { get; set; } = new List<Photo>();

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
