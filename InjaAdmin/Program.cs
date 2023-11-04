using Syncfusion.Blazor;
using InjaAdmin.Shared;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Popups;
using Serilog;
using System.Text.Json.Nodes;
using Blazored.SessionStorage;
using InjaAdmin.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

#region Set the Corrent Database Connection String
try
{
	Log.Information("Reading dbConfig.json");
	if (!File.Exists("dbConfig.json"))
	{
		Log.Error("Fatal Error. Can't find the dbConfig.json file");
		return;
	}

	var jsonObject = JsonNode.Parse(File.ReadAllText("dbConfig.json"));
	if (jsonObject == null)
	{
		Log.Error("jSonObject is null, please check dbConfig.json file and location");
		return;
	}

	var strMainConn = jsonObject["DB"]!.ToString();
	Log.Information(jsonObject.ToJsonString());
	InjaData.Helper.CS = strMainConn;

  // Setup QR App
  InjaAdmin.Helper.strURL = jsonObject["QR_URL"]!.ToString();
  Log.Information(strMainConn);
}
catch (Exception e)
{
	Log.Error(e, "Error Reading DatabaseFile, Application can't run. Exiting");
	return;
}
#endregion

// SetUp Serilog
builder.Host.UseSerilog((ctx, lc) => lc
	.WriteTo.Console()
	.WriteTo.PostgreSQL(InjaData.Helper.CS, "Logs", needAutoCreateTable: true)
	.ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
var supportedCultures = new[] { "en-US", "de", "fr", "ar", "zh" };
var localizationOptions = new RequestLocalizationOptions()
  .SetDefaultCulture(supportedCultures[0])
  .AddSupportedCultures(supportedCultures)
  .AddSupportedUICultures(supportedCultures);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
  "Mjc5NDMzMEAzMjMzMmUzMDJlMzBPWVI1Vnh4MWlEWFptSUs3ZGx4aUI4MkFZaitVVEN5SGtaWUhuWVVDbHNZPQ==");

builder.Services.AddDbContext<InjaData.Models.dbContext>(options => options.UseNpgsql(InjaData.Helper.CS));


builder.Services.AddBlazoredSessionStorage();

builder.Services.AddRazorPages();
builder.Services.AddSignalR(e => { e.MaximumReceiveMessageSize = 102400000; });
builder.Services.AddServerSideBlazor()
	.AddHubOptions(options =>
	{
		options.EnableDetailedErrors = true;
		options.ClientTimeoutInterval = TimeSpan.FromMinutes(1); // Ajusta el tiempo de espera según tus necesidades
		options.KeepAliveInterval = TimeSpan.FromSeconds(10); // Ajusta el intervalo de keep-alive según tus necesidades
	});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<SfDialogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();