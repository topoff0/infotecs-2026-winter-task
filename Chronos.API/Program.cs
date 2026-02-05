using Chronos.API.Extensions;
using Chronos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddMySwagger();

builder.Services.AddInfrastructure(builder.Configuration);

// ====================== APP ==========================
var app = builder.Build();

await app.Services.ApplyMigrationAsync();

app.UseMySwagger(app.Environment);

app.Run();

