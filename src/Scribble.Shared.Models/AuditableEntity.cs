namespace Scribble.Shared.Models;

public abstract class AuditableEntity : Entity, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdateAt { get; set; }
    public string? UpdatedBy { get; set; }
}