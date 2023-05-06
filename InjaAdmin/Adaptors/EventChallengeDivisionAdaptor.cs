using System.Collections;
using InjaData.Models;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace InjaAdmin.Adaptors;

public class EventChallengeDivisionAdaptor : DataAdaptor
{
  private dbContext _db = Helper.DB;

  public override object Read(DataManagerRequest dm, string key = null)
  {
    IEnumerable ds = _db.VEventchallengedivisions.ToList();
    var dataObject = new DataResult();
    if (dm.Search is { Count: > 0 })
    {
      // Searching
      ds = DataOperations.PerformSearching(ds, dm.Search);
    }

    if (dm.Sorted is { Count: > 0 })
    {
      // Sorting
      ds = DataOperations.PerformSorting(ds, dm.Sorted);
    }

    if (dm.Where is { Count: > 0 })
    {
      // Filtering
      ds = DataOperations.PerformFiltering(ds, dm.Where, dm.Where[0].Operator);
    }

    var count = ds.Cast<VEventchallengedivision>().Count();
    if (dm.Skip != 0)
    {
      //Paging
      ds = DataOperations.PerformSkip(ds, dm.Skip);
    }

    if (dm.Take != 0)
    {
      ds = DataOperations.PerformTake(ds, dm.Take);
    }

    if (dm.Group == null) return dm.RequiresCounts ? new DataResult() { Result = ds, Count = count } : ds;
    IEnumerable groupData = Enumerable.Empty<object>();
    ds = dm.Group.Aggregate(ds, (current, group) => DataUtil.Group<VEventchallengedivision>(current, group, dm.Aggregates, 0, dm.GroupByFormatter));
    dataObject.Result = ds; 
    dataObject.Count = count; 
    return dm.RequiresCounts ? dataObject : ds;

  }
}