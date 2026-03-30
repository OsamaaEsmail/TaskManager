namespace TaskManager.Application.DTOs;

// SRP: only carries data needed to CREATE a project
public record CreateProjectDto(
    string Name,
    string Description
);