using InjaData.Models;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class DivisionAdaptor : DataAdaptor
{
	dbContext _db = new dbContext();
	/// <summary>
	/// Method for read data from database
	/// </summary>
	public override object Read(DataManagerRequest dm, string key = null)
	{
		IEnumerable<Division> DataSource = _db.Divisions.ToList();
		if (dm.Search != null && dm.Search.Count > 0)
		{
			// Searching
			DataSource = DataOperations.PerformSearching(DataSource, dm.Search);
		}
		if (dm.Sorted != null && dm.Sorted.Count > 0)
		{
			// Sorting
			DataSource = DataOperations.PerformSorting(DataSource, dm.Sorted);
		}
		if (dm.Where != null && dm.Where.Count > 0)
		{
			// Filtering
			DataSource = DataOperations.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
		}
		int count = DataSource.Cast<Division>().Count();
		if (dm.Skip != 0)
		{
			//Paging
			DataSource = DataOperations.PerformSkip(DataSource, dm.Skip);
		}
		if (dm.Take != 0)
		{
			DataSource = DataOperations.PerformTake(DataSource, dm.Take);
		}
		return dm.RequiresCounts ? new DataResult() { Result = DataSource, Count = count } : (object)DataSource;
	}
	/// <summary>
	/// Method for insert data to database
	/// </summary>
	public override object Insert(DataManager dataManager, object value, string key)
	{
		_db.Divisions.Add(value as Division);
		_db.SaveChanges();
		return value;
	}
	/// <summary>
	/// Method for update data to database
	/// </summary>
	public override object Update(DataManager dataManager, object value, string keyField, string key)
	{
		var ModelValue = (value as Division);
		var data = _db.Divisions.Where(x => x.Id == ModelValue.Id).SingleOrDefault();
		data.Name = ModelValue.Name;
		data.Active = ModelValue.Active;
		_db.SaveChanges();
		return value;
	}
	/// <summary>
	/// Method for remove data from database
	/// </summary>
	public override object Remove(DataManager dataManager, object value, string keyField, string key)
	{
		Division ord = _db.Divisions.Find((int)value);
		_db.Divisions.Remove(ord);
		_db.SaveChanges();
		return value;
	}
}
