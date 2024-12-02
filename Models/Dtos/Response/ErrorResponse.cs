using static SampleCrud_ASPNET.Models.Utils.Error;

namespace SampleCrud_ASPNET.Models.Dtos.Response;

public class ErrorResponse
{
    public bool Success { get; init; } = false;
    public ErrorType Title { get; init; }
    public string Message { get; init; } = string.Empty;
    public Dictionary<string, string>? Details { get; init; }
}
