using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using InjaAPI;
using Microsoft.OpenApi.Models;
using System.Text.Json.Nodes;

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
  Helper.APIDomain = jsonObject["APIDomain"]!.ToString();

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

Log.Information("Start Application {Now}", DateTime.Now);

builder.Services.AddDbContext<InjaData.Models.dbContext>(options => options.UseNpgsql(InjaData.Helper.CS));
Log.Information("DB Context Setted {Cs}", InjaData.Helper.CS);

// For JWT
builder.Services.AddScoped<TokenService, TokenService>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Enable CORS
builder.Services.AddCors();

builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Inja-API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
						Type=ReferenceType.SecurityScheme,
						Id="Bearer"
				}
			},
		new string[]{}
		}
		});
});

builder.Services
		.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ClockSkew = TimeSpan.Zero,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = Helper.JWTIssuer,
				ValidAudience = Helper.JWTAudience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Helper.TokenSecret))
			};
		});

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
