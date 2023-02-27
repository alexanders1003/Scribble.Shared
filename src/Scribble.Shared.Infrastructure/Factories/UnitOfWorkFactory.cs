using Scribble.Shared.Infrastructure.Options;

namespace Scribble.Shared.Infrastructure.Factories;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IConnectionFactory _connectionFactory;

    public UnitOfWorkFactory(IConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public Task<IUnitOfWork> CreateAsync(bool transactional = false, CancellationToken token = default)
        => CreateAsync(RetryOptions.Default, transactional, token);

    public async Task<IUnitOfWork> CreateAsync(RetryOptions retryOptions, bool transactional = false, CancellationToken token = default)
    {
        var connection = await _connectionFactory
            .CreateConnectionAsync(token)
            .ConfigureAwait(false);

        return new UnitOfWork(connection, transactional, retryOptions);
    }
}