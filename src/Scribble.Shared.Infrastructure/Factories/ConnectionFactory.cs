using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Scribble.Posts.Infrastructure.Factories;

namespace Scribble.Shared.Infrastructure.Factories;

public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;

    public ConnectionFactory(IConfiguration configuration)
        => _configuration = configuration;

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = SqlClientFactory.Instance.CreateConnection();
        
        ArgumentNullException.ThrowIfNull(connection, "Some error occured while initializing the connection");

        connection.ConnectionString = _configuration
            .GetConnectionString("Default");
        
        await connection.OpenAsync(token)
            .ConfigureAwait(false);

        return connection;
    }
}