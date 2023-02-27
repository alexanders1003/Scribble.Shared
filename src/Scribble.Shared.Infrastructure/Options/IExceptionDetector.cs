namespace Scribble.Shared.Infrastructure.Options;

public interface IExceptionDetector
{
    bool ShouldRetryRequestOn(Exception exp);
}