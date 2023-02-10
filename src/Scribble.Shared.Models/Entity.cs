namespace Scribble.Shared.Models;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}

public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}