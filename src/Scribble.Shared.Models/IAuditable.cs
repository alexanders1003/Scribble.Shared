namespace Scribble.Shared.Models;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    DateTime? UpdateAt { get; set; }
    string? UpdatedBy { get; set; }
}