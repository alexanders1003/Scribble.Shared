using System.Data;
using Scribble.Shared.Infrastructure.Options;

namespace Scribble.Shared.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private IDbConnection _connection;
    private IDbTransaction? _transaction;

    private readonly RetryOptions? _retryOptions;

    internal UnitOfWork(IDbConnection connection, bool transactional, RetryOptions? retryOptions)
    {
        _connection = connection;
        _transaction = transactional
            ? connection.BeginTransaction()
            : default;

        _retryOptions = retryOptions;
    }

    public async Task ExecuteAsync(IDbRequest request, CancellationToken token = default)
    {
        await RetryRequest
            .HandleRequest(async () => await request
                    .ExecuteAsync(_connection, _transaction, token), _retryOptions);
    }

    public async Task<TResponse?> ExecuteAsync<TResponse>(IDbRequest<TResponse> request, CancellationToken token = default)
    {
        return await RetryRequest
            .HandleRequest(async () => await request
                .ExecuteAsync(_connection, _transaction, token), _retryOptions);
    }

    public void Commit() => _transaction?.Commit();

    public void Rollback() => _transaction?.Rollback();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        
        if (disposing)
        {
            _connection.Dispose();
            _transaction?.Dispose();
        }

        _connection = null!;
        _transaction = null!;

        _disposed = true;
    }
}