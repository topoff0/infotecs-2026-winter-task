namespace Chronos.Application.Features.ResultsData.DTOs.Filters;

public sealed record LastResultsByFileFilter(string FileName, int Count = 10);
