using TaskManager.Core.DTOs.Project;

namespace TaskManager.Core.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetUserProjectsAsync(int userId);
        Task<ProjectResponse?> GetProjectByIdAsync(int userId, int projectId);
        Task<ProjectResponse> CreateProjectAsync(CreateProjectRequest request, int userId);
        Task<ProjectResponse?> UpdateProjectAsync(UpdateProjectRequest request, int userId, int projectId);
        Task<bool> DeleteProjectAsync(int projectId,int userId);
    }
}
