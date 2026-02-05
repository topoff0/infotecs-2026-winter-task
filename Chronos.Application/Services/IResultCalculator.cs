using Chronos.Application.Features.ResultsData.DTOs.Requests;
using Chronos.Core.Entities;

namespace Chronos.Application.Services;

public interface IResultCalculator
{
    ResultEntity Calculate(CalculateResultDto dto);
}
