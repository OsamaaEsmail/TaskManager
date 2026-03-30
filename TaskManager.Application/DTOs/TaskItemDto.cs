namespace TaskManager.Application.DTOs;

// SRP: one job — carry task data to the outside world
// Clean Architecture: outer layers see DTOs, not Entities
public record TaskItemDto(
    Guid Id,
    string Title,
    string Description,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime? CompletedAt
);