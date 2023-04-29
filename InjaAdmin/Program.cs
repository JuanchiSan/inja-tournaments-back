using InjaAdmin.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;
using System.Globalization;
using InjaAdmin.Shared;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
           var supportedCultures = new[] { "en-US", "de", "fr", "ar", "zh" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqUUdrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQllhQX9bdEFjX31bcHA=;Mgo+DSMBPh8sVXJ1S0d+WFBPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXpRfkRrW3tednZTQWQ=;ORg4AjUWIQA/Gnt2VFhhQlVFfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5XdE1iUX1acHJXTmJY;MTg3MzE4NUAzMjMxMmUzMTJlMzQzMUQwb3Zna1hkbFRWRkZkbS9WeFNXTm8rYWozaEZmVWtzTDFhM3RiYjVrZUE9;MTg3MzE4NkAzMjMxMmUzMTJlMzQzMUJtbHZrSlV5cWgzdURjNElQbS83UUx1WDdmVnl6a01hYmdaMkg1TkJ1VW89;NRAiBiAaIQQuGjN/V0d+XU9Ad1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TckdrWHdcdHRTRWVfVQ==;MTg3MzE4OEAzMjMxMmUzMTJlMzQzMWs5QllOUEwzVnRPN2NXaklCU29rR01oZU1DVjU0a1BqNjlaTElVcnF6NVU9;MTg3MzE4OUAzMjMxMmUzMTJlMzQzMW1yQjdaazNLaUxTRkMrTEdYZ3FKbnRRNGN2d09xUTZDL0dudzUrR2hQdGs9;Mgo+DSMBMAY9C3t2VFhhQlVFfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5XdE1iUX1acHJRRWBY;MTg3MzE5MUAzMjMxMmUzMTJlMzQzMVNnU05LMXRKZjlFakl2cnFwWDVUa2pLUmxtZUJZb210c3lJR2FJU0paWDg9;MTg3MzE5MkAzMjMxMmUzMTJlMzQzMURNaGFTRGEyYUQ2VkRsNFhJc0p1ajVpRkZtQ01QK2xienoyaTJyZE1SRDQ9;MTg3MzE5M0AzMjMxMmUzMTJlMzQzMWs5QllOUEwzVnRPN2NXaklCU29rR01oZU1DVjU0a1BqNjlaTElVcnF6NVU9");
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
            app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
