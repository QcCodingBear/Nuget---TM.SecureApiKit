namespace TM.SecureApiKit;

public class ErrorHandlingOptions
{
    public bool IncludeExceptionType { get; set; } = true;
    public bool IncludeExceptionMessage { get; set; } = true;
    public string? SupportEmail { get; set; }
    public bool IncludeExceptionDetails { get; set; } = false;
}