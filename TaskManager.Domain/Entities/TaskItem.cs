namespace TaskManager.Domain.Entities;

// Clean Architecture: Domain is the innermost layer — no dependencies on anything
// Onion Architecture: this is the core — if you change the database or UI, this stays the same
public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}