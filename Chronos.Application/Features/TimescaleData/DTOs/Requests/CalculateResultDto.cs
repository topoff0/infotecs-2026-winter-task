using Chronos.Core.Entities;

namespace Chronos.Application.Features.TimescaleData.DTOs.Requests;

public record class CalculateResultDto(string FileName, IReadOnlyList<ValueEntity> Values);
