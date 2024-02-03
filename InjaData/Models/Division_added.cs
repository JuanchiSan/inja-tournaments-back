using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Division
{
  public string? NameWithCompetitionType {
    get
    {
      var result = Name;
      if (Competitiontype != null)
      {
        result += " - " + Competitiontype.Name;
      }
      return result;
    }
  } 
}
