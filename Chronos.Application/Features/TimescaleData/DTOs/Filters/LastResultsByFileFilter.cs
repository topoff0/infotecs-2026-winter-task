namespace Chronos.Application.Features.TimescaleData.DTOs.Filters;

public sealed record LastResultsByFileNameFilter(string FileName, int Count = 10);
