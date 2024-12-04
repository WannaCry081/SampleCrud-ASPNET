using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SampleCrud_ASPNET.Models.Response;
using SampleCrud_ASPNET.Models.Utils;

namespace SampleCrud_ASPNET.Controllers.Utils;

public static class ControllerUtil
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

    public static IActionResult GetErrorActionResult<T>(ApiResponse<T> apiResponse)
    {
        var title = apiResponse.Title;

        return title switch
        {
            Error.ErrorType.BadRequest => new BadRequestObjectResult(apiResponse),
            Error.ErrorType.ValidationError => new BadRequestObjectResult(apiResponse),
            Error.ErrorType.NotFound => new NotFoundObjectResult(apiResponse),
            Error.ErrorType.Unauthorized => new UnauthorizedObjectResult(apiResponse),
            Error.ErrorType.InternalServer => new StatusCodeResult(500),
            _ => new BadRequestObjectResult(apiResponse)
        };
    }
}
