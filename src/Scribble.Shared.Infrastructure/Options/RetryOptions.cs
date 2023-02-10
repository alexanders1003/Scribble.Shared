using Scribble.Shared.Infrastructure.Options;

namespace Scribble.Posts.Infrastructure.Options;

public class RetryOptions
{
    public static RetryOptions Default { get; }

    static RetryOptions()
        => Default = new RetryOptions(1, 100);
    
    public RetryOptions(int maxRetries, int waitMillis, IExceptionDetector? exceptionDetector = null)
    {
        if (maxRetries < 1)
            throw new ArgumentException($"{nameof(maxRetries)} cannot be less than one", nameof(maxRetries));
        if (waitMillis < 1)
            throw new ArgumentException($"{nameof(waitMillis)} cannot be less than one", nameof(waitMillis));
        
        MaxRetries = maxRetries;
        WaitMillis = waitMillis;

        ExceptionDetector = exceptionDetector ?? new SqlTransientExceptionDetector();
    }
    
    public int MaxRetries { get; }
    public int WaitMillis { get; }
    public IExceptionDetector ExceptionDetector { get; }
}