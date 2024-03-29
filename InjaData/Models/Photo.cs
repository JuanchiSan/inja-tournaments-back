﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Photo
{
    public int Eventid { get; set; }

    public int Challengeid { get; set; }

    public int Contenderid { get; set; }

    public int Photographerid { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public string? Filename { get; set; }

    public string StoredFileName { get; set; } = null!;

    public string? PhotoUrl { get; set; }

    public int Divisionid { get; set; }

    public virtual Challengetype Challenge { get; set; } = null!;

    public virtual Injauser Contender { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Injauser Photographer { get; set; } = null!;
}
