using System.Reflection;
using InjaData.Models;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class CityAdaptor : DataAdaptor
{
  private readonly dbContext _db = new();
  
  public override object Read(DataManagerRequest dm, string? key = null)
  {
    return Helper.ReadDM<City>(_db.Cities.ToList(), dm, key);
  }

  public override object Insert(DataManager dataManager, object value, string key)
  {
    if (value is not City item) return value;
    _db.Cities.Add(item);
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
    if (value is not City item) return value;
    
    var data = _db.Cities.SingleOrDefault(x => x.Id == item.Id);
    if (data == null) return value;
    
    data.Name = item.Name;
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
  /// <summary>
  /// Method for remove data from database
  /// </summary>
  public override object Remove(DataManager dataManager, object value, string keyField, string key)
  {
    var ord = _db.Cities.Find((int)value);
    if (ord == null) return value;
    _db.Cities.Remove(ord);
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
