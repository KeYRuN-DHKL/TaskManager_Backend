using TaskManager.Core.DTOs.AppTask;

namespace TaskManager.Core.Interfaces
{
    public interface IAppTaskService
    {
        Task<IEnumerable<TaskResponse>> GetTasksByProjectAsync(TaskFilterParameters filter);
        Task<TaskResponse?> GetTaskByIdAsync(int taskId);
        Task<List<TaskResponse>> CreateTaskAsync(List<CreateTaskRequest> request);
        Task<TaskResponse> UpdateTaskAsync(int taskId,UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(int taskId);
        Task<IEnumerable<TaskResponse>> GetOverdueTaskAsync(int userId);
        Task<Dictionary<string, int>> GetTaskSummaryAsync(int projectId);
    }
}
