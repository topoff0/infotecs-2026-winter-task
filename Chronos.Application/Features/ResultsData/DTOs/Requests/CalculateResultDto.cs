using Chronos.Core.Entities;

namespace Chronos.Application.Features.ResultsData.DTOs.Requests;

public record class CalculateResultDto(string FileName, IReadOnlyList<ValueEntity> Values);
