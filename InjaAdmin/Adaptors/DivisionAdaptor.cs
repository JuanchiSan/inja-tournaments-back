using System.Reflection;
using InjaData.Models;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class DivisionAdaptor : DataAdaptor
{
	readonly dbContext _db = new dbContext();
	/// <summary>
	/// Method for read data from database
	/// </summary>
	public override object Read(DataManagerRequest dm, string? key = null)
	{
		return Helper.ReadDM<Division>(_db.Divisions.ToList(), dm, key);
	}

	public override object Insert(DataManager dataManager, object value, string key)
	{
		if (value is not Division item) return value;
		
		_db.Divisions.Add(item);
		try
		{
			_db.SaveChanges();
		}
		catch (Exception e)
		{
			var m = MethodBase.GetCurrentMethod();
			Serilog.Log.Error(e, "Error on {MName}", m != null ? m.Name : string.Empty);
		}
		return value;
	}
	
	public override object Update(DataManager dataManager, object value, string keyField, string key)
	{
		if (value is not Division modelValue) return value;

		var data = _db.Divisions.SingleOrDefault(x => x != null && x.Id == modelValue.Id);

		if (data == null) return value;
		
		data.Name = modelValue.Name;
		data.Competitiontypeid = modelValue.Competitiontypeid;
		data.Active = modelValue.Active;
		
		try
		{
			_db.SaveChanges();
		}
		catch (Exception e)
		{
			var m = MethodBase.GetCurrentMethod();
			Serilog.Log.Error(e, "Error on {MName}", m != null ? m.Name : string.Empty);
		}
		return value;
	}
	
	public override object Remove(DataManager dataManager, object value, string keyField, string key)
	{
		try
		{
			var ord = _db.Divisions.Find((int)value);
			_db.Divisions.Remove(ord);
			_db.SaveChanges();
		}
		catch (Exception e)
		{
			var m = MethodBase.GetCurrentMethod();
			Serilog.Log.Error(e, "Error on {MName}", m != null ? m.Name : string.Empty);
		}
		return value;
	}
}
