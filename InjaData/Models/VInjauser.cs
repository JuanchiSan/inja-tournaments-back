﻿using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VInjauser
{
    public int? Injauserid { get; set; }

    public int? Usertypeid { get; set; }

    public int? Eventid { get; set; }

    public string? Injausername { get; set; }

    public string? Mail { get; set; }

    public string? Typename { get; set; }

    public string? Eventname { get; set; }
}