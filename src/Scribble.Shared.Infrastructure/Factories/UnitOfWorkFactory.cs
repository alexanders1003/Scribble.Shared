using Scribble.Posts.Infrastructure.Factories;
using Scribble.Posts.Infrastructure.Options;

namespace Scribble.Shared.Infrastructure.Factories;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IConnectionFactory _connectionFactory;

    public UnitOfWorkFactory(IConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public Task<IUnitOfWork> CreateAsync(CancellationToken token = default)
    {
        return CreateAsync(RetryOptions.Default, token);
    }

    public async Task<IUnitOfWork> CreateAsync(RetryOptions retryOptions, CancellationToken token = default)
    {
        var connection = await _connectionFactory
            .CreateConnectionAsync(token)
            .ConfigureAwait(false);

        return new UnitOfWork(connection, retryOptions);
    }
}