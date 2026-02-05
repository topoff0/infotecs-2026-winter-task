namespace Chronos.API.Contracts.Requests;

public record ProcessFileRequest(string FileName, IFormFile File);
