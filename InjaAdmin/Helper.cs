using System.Collections;
using HarfBuzzSharp;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;

namespace InjaAdmin;

public static class Helper
{
	public static string AppName =>"BeautyComp Admin";
	
	public static string Environment { get; set; }

	public static string[] Languages => new[] { "En", "Es", "It", "Fr", "Pr" };
	
	public static string LengEnglish => Languages[0]; 
	
	public static NumericEditCellParams DFNumericEditCell => new()
	{
		Params = new NumericTextBoxModel<object>
			{ CssClass = "rightalign-numeric-class", ShowClearButton = true, ShowSpinButton = false }
	};
	
	public static NumericEditCellParams DFNumericEditCell2 => new()
	{
		Params = new NumericTextBoxModel<object>
			{ CssClass = "rightalign-numeric-class", ShowClearButton = true, ShowSpinButton = false, Decimals = 2}
	};
	
	public static string paramDetailPointView => "paramdetailview";

	public static string paramChellengeName => "paramChallengeName";

	public static string paramJudgeChellengeName => "paramJudgeChallengeName";

	public static string paramUserSession => "UserSession";
	
	public static string strURL { get; set; } = string.Empty;

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
