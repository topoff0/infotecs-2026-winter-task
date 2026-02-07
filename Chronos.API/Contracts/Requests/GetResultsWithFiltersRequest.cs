using Chronos.Application.Features.TimescaleData.DTOs.Filters;

namespace Chronos.API.Contracts.Requests;

public record GetResultsWithFiltersRequest(ResultFilters Filters);
