using TaskManager.Application.DTOs.Requests;
using TaskManager.Application.DTOs.Responses;

namespace TaskManager.Application.Interfaces;

// DIP: Console depends on this — not on TaskService directly
// ISP: only task use cases — separate from project
public interface ITaskService
{
    TaskItemResponse? GetById(Guid id);
    List<TaskItemResponse> GetByProjectId(Guid projectId);
    TaskItemResponse Create(CreateTaskRequest request);
    void Complete(Guid id);
    void Reopen(Guid id);
    void Delete(Guid id);
}