namespace Doyep.Analyzer.Application;

/// <summary>
/// Represents an error that can occur during the execution of an operation, containing a code and a message describing the error.
/// </summary>
public class Error
{
    /// <summary>
    /// Gets the code representing the type of error that occurred.
    /// This can be used to categorize errors and provide more specific handling based on the error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the message describing the error that occurred.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets or sets the HTTP status code associated with this error, which can be used when returning error responses in an API context.
    /// </summary>
    public int StatusCode { get; set; }

    public Error(string code, string message, int statusCode = 500)
    {
        Code = code;
        Message = message;
        StatusCode = statusCode;
    }
}
