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

    public const string BAD_REQUEST = "Request body is invalid or malformed.";
    public const string VALIDATION_ERROR = "Validation failed on one or more fields.";

    public static string CREATING_RESOURCE(string entity)
    {
        return $"Failed to create '{entity}' resource";
    }
}
