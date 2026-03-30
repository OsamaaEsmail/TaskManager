namespace TaskManager.Application.DTOs;

// SRP: one job — carry project data outside
public record ProjectDto(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    int TotalTasks,
    int CompletedTasks
);