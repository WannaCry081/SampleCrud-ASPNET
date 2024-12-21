namespace SampleCrud_ASPNET.Models.Dtos.Response;

/// <summary>
///     DTO for success response.
/// </summary>
public class SuccessResponse<T>
{
    public bool Success { get; init; } = true;
    public T? Data { get; init; }
    public string Message { get; init; } = string.Empty;
}
