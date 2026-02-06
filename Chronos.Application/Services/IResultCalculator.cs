using Chronos.Application.Features.TimescaleData.DTOs.Requests;
using Chronos.Core.Entities;

namespace Chronos.Application.Services;

public interface IResultCalculator
{
    ResultEntity Calculate(CalculateResultDto dto);
}
