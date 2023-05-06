namespace InjaAdmin;

public static class Helper
{
	private static InjaData.Models.dbContext? _db;

	public static InjaData.Models.dbContext DB
	{
		get
		{
			_db ??= new InjaData.Models.dbContext();
			return _db;
		}
	}

	public static void ResetContext()
	{
		_db = new InjaData.Models.dbContext();
		GC.Collect();
	}
}
