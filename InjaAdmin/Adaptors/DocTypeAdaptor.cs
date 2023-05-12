using System.Reflection;
using InjaData.Models;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;

namespace InjaAdmin.Adaptors
{
  public class DocTypeAdaptor : DataAdaptor
  {
    private readonly dbContext _db = new();
    /// <summary>
    /// Method for read data from database
    /// </summary>
    public override object Read(DataManagerRequest dm, string? key = null)
    {
      return Helper.ReadDM<Doctype>(_db.Doctypes.ToList(), dm, key);
    }
    /// <summary>
    /// Method for insert data to database
    /// </summary>
    public override object Insert(DataManager dataManager, object value, string key)
    {
      if (value is not Doctype item) return value;
      _db.Doctypes.Add(item);
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
      if (value is not Doctype modelValue) return value;
     
      var data = _db.Doctypes.SingleOrDefault(x => x.Id == modelValue.Id);
      if (data == null) return value;
      data.Name = modelValue.Name;
      data.Active = modelValue.Active;
      try
      {
        _db.SaveChanges();
      }
      catch (Exception e)
      {
        Serilog.Log.Error(e, "Error at Update Doctype");
      }
      
      return value;
    }
    /// <summary>
    /// Method for remove data from database
    /// </summary>
    public override object Remove(DataManager dataManager, object value, string keyField, string key)
    {
      var ord = _db.Doctypes.Find((int)value);
      if (ord == null) return value;
      _db.Doctypes.Remove(ord);
      _db.SaveChanges();
      return value;
    }
  }
}
