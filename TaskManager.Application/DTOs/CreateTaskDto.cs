namespace TaskManager.Application.DTOs;

// SRP: only carries data needed to CREATE a task
public record CreateTaskDto(
    string Title,
    string Description,
    Guid ProjectId
);