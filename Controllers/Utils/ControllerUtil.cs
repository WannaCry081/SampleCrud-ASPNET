using Microsoft.AspNetCore.Mvc.ModelBinding;
using SampleCrud_ASPNET.Models.Response;
using SampleCrud_ASPNET.Models.Utils;

namespace SampleCrud_ASPNET.Controllers.Utils;

public class ControllerUtil
{
    public static ApiResponse<T> ValidateState<T>(ModelStateDictionary modelState)
    {
        var details = modelState
            .Where(ms => ms.Value!.Errors.Count > 0)
            .ToDictionary(
                kvp => char.ToLower(kvp.Key[0]) + kvp.Key[1..],
                kvp => string.Join("; ", kvp.Value!.Errors.Select(e => e.ErrorMessage))
            );

        return ApiResponse<T>.ErrorResponse(
            Error.ErrorType.ValidationError,
            Error.VALIDATION_ERROR,
            details
        );
    }
}
