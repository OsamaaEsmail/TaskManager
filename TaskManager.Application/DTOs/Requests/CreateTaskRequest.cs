namespace TaskManager.Application.DTOs.Requests;

// SRP: only carries data needed to CREATE a task
public record CreateTaskRequest(
    string Title,
    string Description,
    Guid ProjectId
);