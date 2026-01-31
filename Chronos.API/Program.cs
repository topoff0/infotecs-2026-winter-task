using Chronos.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddMySwagger();

// ====================== APP ==========================
var app = builder.Build();

app.UseMySwagger(app.Environment);

app.Run();

