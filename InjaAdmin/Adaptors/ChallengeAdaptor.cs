using System.Reflection;
using Syncfusion.Blazor;
using InjaData.Models;

namespace InjaAdmin.Adaptors;

public class ChallengeAdaptor : DataAdaptor
{
  readonly dbContext _db = new();
  
  public override object Read(DataManagerRequest dm, string? key = null)
  {
    return Helper.ReadDM<Challengetype>(_db.Challengetypes.ToList(), dm, key);
  }

  public override object Insert(DataManager dataManager, object value, string key)
  {
    if (value is not Challengetype item) return value;
    
    _db.Challengetypes.Add(item);

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
    if (value is not Challengetype modelValue) return value;

    var data = _db.Challengetypes.SingleOrDefault(x => x.Id == modelValue.Id);
    if (data == null) return value;
    
    data.Name = modelValue.Name;
    data.Comment = modelValue.Comment;
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
    var ord = _db.Challengetypes.Find((int)value);
    if (ord == null) return value;
    
    _db.Challengetypes.Remove(ord);
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
