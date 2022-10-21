using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

InjaData.Helper.CS = builder.Configuration["ConnectionStrings:WebApiDatabase"];

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InjaData.Models.dbContext>(options =>
	options.UseNpgsql("Host=10.10.12.11; Database=postgres; Username=postgres; Password=Qwert.789"));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
