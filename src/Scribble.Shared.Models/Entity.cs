namespace Scribble.Shared.Models;

public abstract class Entity : IEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public bool Equals(IEntity<Guid>? other)
    {
        return Id.Equals(other!.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((IEntity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; } = default!;

    public bool Equals(IEntity<TKey>? other)
    {
        return EqualityComparer<TKey>.Default.Equals(Id, other!.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Entity<TKey>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TKey>.Default.GetHashCode(Id);
    }
}