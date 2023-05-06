using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using InjaData.Models;

namespace InjaAdmin.Adaptors;

public class ChallengeAdaptor : DataAdaptor
{
  dbContext _db = new dbContext();
  /// <summary>
  /// Method for read data from database
  /// </summary>
  public override object Read(DataManagerRequest dm, string? key = null)
  {
    IEnumerable<Challengetype> DataSource = _db.Challengetypes.ToList();
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
    int count = DataSource.Cast<Challengetype>().Count();
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
    _db.Challengetypes.Add(value as Challengetype);
    _db.SaveChanges();
    return value;
  }
  /// <summary>
  /// Method for update data to database
  /// </summary>
  public override object Update(DataManager dataManager, object value, string keyField, string key)
  {
    var ModelValue = (value as Challengetype);
    var data = _db.Challengetypes.Where(x => x.Id == ModelValue.Id).SingleOrDefault();
    data.Name = ModelValue.Name;
    data.Comment = ModelValue.Comment;
    data.Active = ModelValue.Active;
    _db.SaveChanges();
    return value;
  }
  /// <summary>
  /// Method for remove data from database
  /// </summary>
  public override object Remove(DataManager dataManager, object value, string keyField, string key)
  {
    Challengetype ord = _db.Challengetypes.Find((int)value);
    _db.Challengetypes.Remove(ord);
    _db.SaveChanges();
    return value;
  }
}
