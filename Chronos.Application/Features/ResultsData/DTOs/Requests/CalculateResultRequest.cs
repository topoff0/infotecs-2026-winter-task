using Chronos.Core.Entities;

namespace Chronos.Application.Features.ResultsData.DTOs.Requests;

public record class CalculateResultRequest(string FileName, IReadOnlyList<ValueEntity> Values);
