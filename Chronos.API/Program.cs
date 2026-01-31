var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// ====================== APP ==========================
var app = builder.Build();

app.Run();

