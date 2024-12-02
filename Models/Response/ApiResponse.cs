using Newtonsoft.Json;

namespace SampleCrud_ASPNET.Models.Response;

public class ApiResponse<T>
{
    public bool Success { get; init; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; init; }
    public string Message { get; init; } = null!;

    public static ApiResponse<T> SuccessResponse(T? data, string message = "")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static ApiResponse<T> ErrorResponse(string message = "")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message
        };
    }
}
