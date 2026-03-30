using TaskManager.Application.DTOs.Requests;
using TaskManager.Application.DTOs.Responses;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

// SRP: only handles project use cases
// DIP: implements IProjectService — Console depends on the interface not this class
// OCP: swap InMemoryStore with a real DB — this class structure stays the same
public class ProjectService : IProjectService
{
    private readonly InMemoryStore _store;

    // DIP: receives the store from outside — doesn't create it
    public ProjectService(InMemoryStore store)
    {
        _store = store;
    }

    public ProjectResponse? GetById(Guid id)
    {
        var project = _store.Projects.FirstOrDefault(p => p.Id == id);
        if (project is null) return null;

        return MapToResponse(project);
    }

    public List<ProjectResponse> GetAll()
    {
        return _store.Projects.Select(MapToResponse).ToList();
    }

    public ProjectResponse Create(CreateProjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Project name cannot be empty.");

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        _store.Projects.Add(project);
        return MapToResponse(project);
    }

    public void Delete(Guid id)
    {
        var project = _store.Projects.FirstOrDefault(p => p.Id == id)
            ?? throw new InvalidOperationException("Project not found.");

        _store.Projects.Remove(project);
    }

    // Entity stays hidden — only Response goes out (Clean Architecture)
    private static ProjectResponse MapToResponse(Project project) => new(
        project.Id,
        project.Name,
        project.Description,
        project.CreatedAt,
        project.Tasks.Count,
        project.Tasks.Count(t => t.IsCompleted)
    );
}