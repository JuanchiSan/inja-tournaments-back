using System.Collections;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace InjaAdmin;

public static class Helper
{
	private static InjaData.Models.dbContext? _db;

	public static InjaData.Models.dbContext DB
	{
		get
		{
			_db ??= new InjaData.Models.dbContext();
			return _db;
		}
	}

	public static string paramDetailPointView => "paramdetailview";
	public static string paramChellengeName => "paramChallengeName";
	
	public static void ResetContext()
	{
		_db = new InjaData.Models.dbContext();
		GC.Collect();
	}
	
	public static object ReadDM<T>(IEnumerable ds, DataManagerRequest dm, string? key = null)
	{
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

		// ReSharper disable once PossibleMultipleEnumeration
		var count = ds.Cast<T>().Count();
		if (dm.Skip != 0) //Paging
		{
			// ReSharper disable once PossibleMultipleEnumeration
			ds = DataOperations.PerformSkip(ds, dm.Skip);
		}
		
		if (dm.Take != 0)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			ds = DataOperations.PerformTake(ds, dm.Take);
		}
		
		if (dm.Group == null) return dm.RequiresCounts ? new DataResult { Result = ds, Count = count } : ds;
		ds = dm.Group.Aggregate(ds, (current, group) => DataUtil.Group<T>(current, group, dm.Aggregates, 0, dm.GroupByFormatter));
		dataObject.Result = ds; 
		dataObject.Count = count; 
		return dm.RequiresCounts ? dataObject : ds;
	}
}
