namespace TaskManager.Application.DTOs.Responses;

// SRP: one job — carry project data outside
public record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    int TotalTasks,
    int CompletedTasks
);