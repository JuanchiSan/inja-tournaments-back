using InjaData.Models;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class EventChallengeDivisionAdaptor : DataAdaptor
{
  private readonly dbContext _db = Helper.DB;

  public override object Read(DataManagerRequest dataManagerRequest, string? additionalParam = null)
  {
    return Helper.ReadDM<VEventchallengedivision>(_db.VEventchallengedivisions.ToList(), dataManagerRequest, additionalParam);
  }
}