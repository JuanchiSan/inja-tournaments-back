using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class VContenderInscriptionsCount
{
    public int? Eventid { get; set; }

    public int? Divisionid { get; set; }

    public int? Contenderid { get; set; }

    public long? ContenderInscriptions { get; set; }

    public long? ContenderInscriptionWithPoints { get; set; }

    public long? TotalChallenges { get; set; }
}
