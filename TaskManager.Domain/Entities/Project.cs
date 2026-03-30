namespace TaskManager.Domain.Entities;

// Clean Architecture: Domain has ZERO references to other projects
// OOP - Association: Project has a list of TaskItems (one-to-many)
public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<TaskItem> Tasks { get; set; } = new();
}