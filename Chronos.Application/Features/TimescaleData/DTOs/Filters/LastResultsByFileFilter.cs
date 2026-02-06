namespace Chronos.Application.Features.TimescaleData.DTOs.Filters;

public sealed record LastResultsByFileFilter(string FileName, int Count = 10);
