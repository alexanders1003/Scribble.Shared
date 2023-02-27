namespace Scribble.Shared.Models;

public interface IEntity : IEntity<Guid> { }
public interface IEntity<TKey> : IEquatable<IEntity<TKey>>
    where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}