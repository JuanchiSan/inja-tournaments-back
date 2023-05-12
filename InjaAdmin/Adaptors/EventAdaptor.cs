using System.Reflection;
using InjaData.Models;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors;

public class EventAdaptor : DataAdaptor
{
  private readonly dbContext _db = new();

  /// <summary>
  /// Method for read data from database
  /// </summary>
  public override object Read(DataManagerRequest dm, string? key = null)
  {
    return Helper.ReadDM<Event>(_db.Events.ToList(), dm, key);
  }

  /// <summary>
  /// Method for insert data to database
  /// </summary>
  public override object Insert(DataManager dataManager, object value, string key)
  {
    if (value is not Event item) return value;
    _db.Events.Add(item);
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
    if (value is not Event item) return value;

    var data = _db.Events.SingleOrDefault(x => x.Id == item.Id);
    if (data == null) return value;

    data.Name = item.Name;
    data.Creatoruserid = item.Creatoruserid;
    data.Startdate = item.Startdate;
    data.Comment = item.Comment;
    data.Enddate = item.Enddate;
    data.Startinscription = item.Startinscription;
    data.Endinscription = item.Endinscription;
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
    var ord = _db.Events.Find((int)value);
    if (ord == null) return value;
    
    _db.Events.Remove(ord);
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