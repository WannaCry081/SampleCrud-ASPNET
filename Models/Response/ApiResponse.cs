using Newtonsoft.Json;
using static SampleCrud_ASPNET.Models.Utils.Error;

namespace SampleCrud_ASPNET.Models.Response;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = null!;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public ErrorType? Title { get; init; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; init; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? Details { get; init; }

    public static ApiResponse<T> SuccessResponse(T? data, string message)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static ApiResponse<T> ErrorResponse(
        ErrorType type, string message, Dictionary<string, string>? details)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Title = type,
            Message = message,
            Details = details
        };
    }
}
