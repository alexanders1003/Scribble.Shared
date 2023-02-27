using System.Data;

namespace Scribble.Shared.Infrastructure;

public interface IDbRequest
{
    Task ExecuteAsync(IDbConnection connection, IDbTransaction? transaction, CancellationToken token = default);
}

public interface IDbRequest<TResponse>
{
    Task<TResponse> ExecuteAsync(IDbConnection connection, IDbTransaction? transaction, CancellationToken token = default);
}
