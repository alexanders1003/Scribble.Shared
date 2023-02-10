namespace Scribble.Shared.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    Task ExecuteAsync(IDbRequest request, CancellationToken token = default);
    Task<T?> ExecuteAsync<T>(IDbRequest<T> request, CancellationToken token = default);
    void Commit();
    void Rollback();
}