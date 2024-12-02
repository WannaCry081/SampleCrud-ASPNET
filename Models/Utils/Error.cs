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
}
