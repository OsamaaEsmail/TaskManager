namespace TaskManager.Application.DTOs.Requests;

// SRP: only carries data needed to CREATE a project
public record CreateProjectRequest(
    string Name,
    string Description
);