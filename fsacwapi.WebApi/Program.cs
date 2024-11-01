using System.Text;
using fsacwapi.WebApi.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes("");
string? connectionString = "Server=localhost;Database=fsacwapi;User Id=postgres;Password=root;";

builder.Services.AddCustomSwagger();
builder.Services.AddCustomServices();
builder.Services.AddCustomDatabase(connectionString);
builder.Services.AddCustomAuthentication(key);

var app = builder.Build();

app.ConfigureSwagger();
app.ConfigureHttpPipeline();

DbInitializer.InitializeDatabase(app);

app.Run();
