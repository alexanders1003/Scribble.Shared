using System.Data;

namespace Scribble.Shared.Infrastructure.Factories;

public interface IConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}