using TaskManager.Application.DTOs.Requests;
using TaskManager.Application.DTOs.Responses;

namespace TaskManager.Application.Interfaces;

// DIP: Console depends on this interface — not on the concrete service
// ISP: only project use cases
public interface IProjectService
{
    ProjectResponse? GetById(Guid id);
    List<ProjectResponse> GetAll();
    ProjectResponse Create(CreateProjectRequest request);
    void Delete(Guid id);
}