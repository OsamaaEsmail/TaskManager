using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

// SRP: one job — hold data in memory
// this is where a real database would be (EF Core DbContext for example)
// if you swap this with a real database, the Services don't change — OCP
public class InMemoryStore
{
    public List<Project> Projects { get; } = new();
    public List<TaskItem> Tasks { get; } = new();
}