using Chronos.API.Extensions;
using Chronos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddMySwagger();

builder.Services.AddInfrastructure(builder.Configuration);

// ====================== APP ==========================
var app = builder.Build();

app.UseGlobalErrorHandler();

await app.Services.ApplyMigrationAsync();

app.UseMySwagger(app.Environment);

app.MapControllers();

app.Run();

