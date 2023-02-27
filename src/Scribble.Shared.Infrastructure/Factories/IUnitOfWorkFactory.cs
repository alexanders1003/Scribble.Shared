using Scribble.Shared.Infrastructure.Options;

namespace Scribble.Shared.Infrastructure.Factories;

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> CreateAsync(bool transactional = false, CancellationToken token = default);
    Task<IUnitOfWork> CreateAsync(RetryOptions options, bool transactional = false, CancellationToken token = default);
}