using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VUser
{
    public int? Injauserid { get; set; }

    public string? Injausername { get; set; }

    public string? Mail { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Phone { get; set; }

    public int? Cityid { get; set; }

    public short? Docid { get; set; }

    public string? Docnumber { get; set; }
}
