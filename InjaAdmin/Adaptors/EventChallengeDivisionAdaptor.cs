﻿using InjaData.Models;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class EventChallengeDivisionAdaptor : DataAdaptor
{
  private readonly dbContext _db = new();

  public override object Read(DataManagerRequest dataManagerRequest, string? additionalParam = null)
  {
    return Helper.ReadDM<VEventchallengedivision>(_db.VEventchallengedivisions.ToList(), dataManagerRequest, additionalParam);
  }

  public override object Remove(DataManager dataManager, object primaryColumnValue, string primaryColumnName, string additionalParam)
  {
    return base.Remove(dataManager, primaryColumnValue, primaryColumnName, additionalParam);
  }
}