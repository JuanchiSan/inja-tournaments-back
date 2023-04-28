using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

InjaData.Helper.CS = builder.Configuration["ConnectionStrings:WebApiDatabase"];

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .CreateBootstrapLogger();

Log.Information($"Start Application {DateTime.Now}");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InjaData.Models.dbContext>(options =>
	options.UseNpgsql(InjaData.Helper.CS));

Log.Information($"DB Context Setted {InjaData.Helper.CS}");

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//	.AddJwtBearer(options =>
//	{
//		options.TokenValidationParameters = new TokenValidationParameters
//		{
//			ValidateIssuer = true,
//			ValidateAudience = true,
//			ValidateLifetime = true,
//			ValidateIssuerSigningKey = true,
//			ValidIssuer = builder.Configuration["Jwt:Issuer"],
//			ValidAudience = builder.Configuration["Jwt:Audience"],
//			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//		};
//	});

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

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
