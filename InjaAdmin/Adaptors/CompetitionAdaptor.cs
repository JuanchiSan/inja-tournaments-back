using System.Reflection;
using Syncfusion.Blazor;
using InjaData.Models;

namespace InjaAdmin.Adaptors;

public class CompetitionAdaptor : DataAdaptor
{
	private readonly dbContext _db = new();

	public override object Read(DataManagerRequest dm, string? key = null)
	{
		return Helper.ReadDM<Competitiontype>(_db.Competitiontypes.ToList(), dm, key);
	}

	public override object Insert(DataManager dataManager, object value, string key)
	{
		if (value is not Competitiontype item) return value;
		_db.Competitiontypes.Add(item);
		
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
		if (value is not Competitiontype item) return value;

		var data = _db.Competitiontypes.SingleOrDefault(x => x.Id == item.Id);
		if (data == null) return value;
		
		data.Name = item.Name;
		data.Comment = item.Comment;
		data.Active = item.Active;
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
		var ord = _db.Competitiontypes.Find((int)value);
		if (ord == null) return value;
		
		_db.Competitiontypes.Remove(ord);
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
}
