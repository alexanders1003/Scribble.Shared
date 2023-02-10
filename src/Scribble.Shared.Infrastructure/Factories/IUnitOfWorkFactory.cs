using Scribble.Posts.Infrastructure.Options;

namespace Scribble.Shared.Infrastructure.Factories;

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> CreateAsync(CancellationToken token = default);
    Task<IUnitOfWork> CreateAsync(RetryOptions options, CancellationToken token = default);
}