namespace Scribble.Shared.Models;

public interface IEntity : IEntity<Guid> { }
public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}