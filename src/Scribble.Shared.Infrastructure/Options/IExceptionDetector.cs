namespace Scribble.Posts.Infrastructure.Options;

public interface IExceptionDetector
{
    bool ShouldRetryRequestOn(Exception exp);
}