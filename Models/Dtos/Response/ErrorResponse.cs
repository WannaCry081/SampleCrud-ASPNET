using static SampleCrud_ASPNET.Models.Utils.Error;

namespace SampleCrud_ASPNET.Models.Dtos.Response;

/// <summary>
///     DTO for error response.
/// </summary>
public class ErrorResponse
{
    public bool Success { get; init; } = false;
    public ErrorType Title { get; init; }
    public string Message { get; init; } = string.Empty;
    public Dictionary<string, string>? Details { get; init; }
}
