using TaskManager.Application.DTOs.Requests;
using TaskManager.Application.DTOs.Responses;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

// SRP: only handles task use cases
// DIP: implements ITaskService — Console depends on the interface
// OCP: change storage without touching this logic
public class TaskService : ITaskService
{
    private readonly InMemoryStore _store;

    public TaskService(InMemoryStore store)
    {
        _store = store;
    }

    public TaskItemResponse? GetById(Guid id)
    {
        var task = _store.Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return null;

        return MapToResponse(task);
    }

    public List<TaskItemResponse> GetByProjectId(Guid projectId)
    {
        var project = _store.Projects.FirstOrDefault(p => p.Id == projectId)
            ?? throw new InvalidOperationException("Project not found.");

        return project.Tasks.Select(MapToResponse).ToList();
    }

    public TaskItemResponse Create(CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Task title cannot be empty.");

        var project = _store.Projects.FirstOrDefault(p => p.Id == request.ProjectId)
            ?? throw new InvalidOperationException("Project not found.");

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _store.Tasks.Add(task);
        project.Tasks.Add(task);

        return MapToResponse(task);
    }

    public void Complete(Guid id)
    {
        var task = _store.Tasks.FirstOrDefault(t => t.Id == id)
            ?? throw new InvalidOperationException("Task not found.");

        if (task.IsCompleted)
            throw new InvalidOperationException("Task is already completed.");

        task.IsCompleted = true;
        task.CompletedAt = DateTime.UtcNow;
    }

    public void Reopen(Guid id)
    {
        var task = _store.Tasks.FirstOrDefault(t => t.Id == id)
            ?? throw new InvalidOperationException("Task not found.");

        if (!task.IsCompleted)
            throw new InvalidOperationException("Task is not completed.");

        task.IsCompleted = false;
        task.CompletedAt = null;
    }

    public void Delete(Guid id)
    {
        var task = _store.Tasks.FirstOrDefault(t => t.Id == id)
            ?? throw new InvalidOperationException("Task not found.");

        _store.Tasks.Remove(task);
    }

    private static TaskItemResponse MapToResponse(TaskItem task) => new(
        task.Id,
        task.Title,
        task.Description,
        task.IsCompleted,
        task.CreatedAt,
        task.CompletedAt
    );
}