namespace SampleCrud_ASPNET.Models.Utils;

public static class Error
{
    public enum ErrorType
    {
        ValidationError,
        BadRequest,
        Unauthorized,
        NotFound,
        InternalServer
    }

    public const string VALIDATION_ERROR = "Validation failed on one or more fields.";
}
