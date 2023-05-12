using System.Reflection;
using InjaData.Models;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class JugmentCriteriaAdaptor : DataAdaptor
{
	private readonly dbContext _db = new();
	/// <summary>
	/// Method for read data from database
	/// </summary>
	public override object Read(DataManagerRequest dm, string? key = null)
	{
		return Helper.ReadDM<Judgmentcriterion>(_db.Judgmentcriteria.ToList(), dm, key);
	}
	/// <summary>
	/// Method for insert data to database
	/// </summary>
	public override object Insert(DataManager dataManager, object value, string key)
	{
		if(value is not Judgmentcriterion item) return value;
		
		_db.Judgmentcriteria.Add(item);
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
	/// <summary>
	/// Method for update data to database
	/// </summary>
	public override object Update(DataManager dataManager, object value, string keyField, string key)
	{
		if (value is not Judgmentcriterion item) return value;
		
		var data = _db.Judgmentcriteria.SingleOrDefault(x => x.Id == item.Id);
		if (data == null) return value;
		data.Name = item.Name;
		data.Namees = item.Namees;
		data.Namefr = item.Namefr;
		data.Namepr = item.Namepr;
		data.Nameit = item.Nameit;
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
	/// <summary>
	/// Method for remove data from database
	/// </summary>
	public override object Remove(DataManager dataManager, object value, string keyField, string key)
	{
		var ord = _db.Judgmentcriteria.Find((int)value);
		if (ord == null) return value;
		_db.Judgmentcriteria.Remove(ord);
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