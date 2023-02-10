namespace Scribble.Posts.Infrastructure.Options;

public static class RetryRequest
{
    public static async Task HandleRequest(Func<Task> action, RetryOptions? retryOptions)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action), $"{nameof(action)} cannot be null");

        if (retryOptions?.ExceptionDetector is null)
        {
            await action();
        }
        else
        {
            var retryCount = 1;
            while (retryCount <= retryOptions.MaxRetries)
            {
                try
                { 
                    await action();
                }
                catch (Exception exp)
                {
                    await HandleExceptionAsync(retryOptions, exp, retryCount);
                }

                retryCount++;
            }
        }
    }
    public static async Task<TResponse?> HandleRequest<TResponse>(Func<Task<TResponse>> func, RetryOptions? retryOptions)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func), $"{nameof(func)} cannot be null");

        if (retryOptions?.ExceptionDetector is null)
            return await func();

        var retryCount = 1;
        while (retryCount <= retryOptions.MaxRetries)
        {
            try
            {
                return await func();
            }
            catch (Exception exp)
            {
                await HandleExceptionAsync(retryOptions, exp, retryCount);
            }

            retryCount++;
        }

        return default;
    }

    private static Task HandleExceptionAsync(RetryOptions options, Exception exp, int retryCount)
    {
        if (!options.ExceptionDetector.ShouldRetryRequestOn(exp) || retryCount >= options.MaxRetries)
            throw exp;

        var sleepTime = TimeSpan.FromMicroseconds(Math.Pow(options.WaitMillis, retryCount));
        return Task.Delay(sleepTime);
    }
}