using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Eventjudgechallengedivision
{
    public int Id { get; set; }

    public int Divisionid { get; set; }

    public int Eventchallengeid { get; set; }

    public int Challengejuzmentcriteria { get; set; }

    public int Judgeid { get; set; }

    public string? Comment { get; set; }

    public bool? Active { get; set; }

    public DateTime Creationdate { get; set; }

    public DateTime? Updateddate { get; set; }

    public virtual Challengejuzmentcriterion ChallengejuzmentcriteriaNavigation { get; set; } = null!;

    public virtual Eventchallengedivision Eventchallengedivision { get; set; } = null!;

    public virtual Injauser Judge { get; set; } = null!;

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();
}
