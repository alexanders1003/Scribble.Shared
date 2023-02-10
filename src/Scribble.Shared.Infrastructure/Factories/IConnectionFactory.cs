using System.Data;

namespace Scribble.Posts.Infrastructure.Factories;

public interface IConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}