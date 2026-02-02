namespace Chronos.Application.Features.ResultsData.DTOs;

public sealed record LastResultsByFileFilter(string FileName, int Count = 10);
