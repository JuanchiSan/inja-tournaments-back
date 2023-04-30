using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Challenge
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Comment { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Challengejuzmentcriterion> Challengejuzmentcriteria { get; set; } = new List<Challengejuzmentcriterion>();

    public virtual ICollection<Eventchallengedivision> Eventchallengedivisions { get; set; } = new List<Eventchallengedivision>();

    public virtual ICollection<Eventchallenge> Eventchallenges { get; set; } = new List<Eventchallenge>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Challenge> Challengeiddependencies { get; set; } = new List<Challenge>();

    public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}
