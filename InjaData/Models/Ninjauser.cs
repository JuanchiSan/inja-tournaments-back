using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Ninjauser
{
    public int Id { get; set; }

    public string Mail { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Docnnmber { get; set; } = null!;

    public short Docid { get; set; }

    public string Usertype { get; set; } = null!;

    public string? Street { get; set; }

    public string? Number { get; set; }

    public string? Phone { get; set; }

    public int? Cityid { get; set; }

    public bool? Active { get; set; }

    public virtual City? City { get; set; }

    public virtual Doctype Doc { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Inscriptionchallenge> Inscriptionchallenges { get; set; } = new List<Inscriptionchallenge>();

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    public virtual ICollection<Persongroup> Persongroups { get; set; } = new List<Persongroup>();

    public virtual ICollection<Point> PointJudges { get; set; } = new List<Point>();

    public virtual ICollection<Point> PointUsers { get; set; } = new List<Point>();

    public virtual ICollection<Usertype> Types { get; set; } = new List<Usertype>();
}
