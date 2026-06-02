using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.DTOs.AppTask;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Services
{
    public class AppTaskService : IAppTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppTaskService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskResponse>> GetTasksByProjectAsync(int projectId,TaskFilterParams filter)
        {
            var query = _unitOfWork.Tasks
                .Query()
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.Assignee)
                .Include(t => t.TaskTag)
                .ThenInclude(tt => tt.Tag)
                .AsQueryable();

            if (filter.Priority.HasValue)
                query = query.Where(t => t.Priority == filter.Priority.Value);

            if (filter.Status.HasValue)
                query = query.Where(t => t.Status == filter.Status.Value);

            if (!String.IsNullOrWhiteSpace(filter.Tag))
                query = query.Where(t => t.TaskTag.Any(tt => tt.Tag.Name == filter.Tag));

            if()



            

            return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
        }

        public async Task<TaskResponse?> GetTaskByIdAsync(int taskId)
        {
            var task = await _unitOfWork.Tasks
                .Query()
                .Include(task => task.Assignee)
                .FirstOrDefaultAsync(task => task.Id ==  taskId);

            return task == null ? null : _mapper.Map<TaskResponse>(task);
        }

        public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request)
        {
            var task = _mapper.Map<AppTaskEntity>(request);

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TaskResponse>(task);
        }

        public async Task<TaskResponse?> UpdateTaskAsync(int taskId,UpdateTaskRequest request)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);

            if (task == null)
                return null;

            task.Title = request.Title;
            task.Description = request.Description;
            task.Priority = request.Priority;
            task.Status = request.Status;
            task.DueDate = request.DueDate;
            task.AssigneeId = request.AssigneeId;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return await GetTaskByIdAsync(taskId);
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);

            if (task == null)
                return false;

            task.IsDeleted = true;

            _unitOfWork.Tasks.Update(task);
            var rowsAffected = await _unitOfWork.SaveChangesAsync();

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        public Task<IEnumerable<TaskResponse>> GetOverdueTaskAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, int>> GetTaskSummaryAsync(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
