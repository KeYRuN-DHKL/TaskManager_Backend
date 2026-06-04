    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Core.TaskManagerExceptions;
    using TaskManager.Core.DTOs.AppTask;
    using TaskManager.Core.Entities;
    using TaskManager.Core.Enum;
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

            public async Task<IEnumerable<TaskResponse>> GetTasksByProjectAsync(int projectId,TaskFilterParameters filter)
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

                if (filter.IsOverdue == true)
                    query = query.Where(t => t.DueDate < DateTime.UtcNow && t.Status != TaskStatusEnum.Done);

                var tasks = query
                    .OrderByDescending(task => task.Priority)
                    .ThenBy(task => task.DueDate)
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .ToListAsync();
            

                return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
            }

            public async Task<IEnumerable<TaskResponse>> GetOverdueTaskAsync(int UserId)
            {
                var tasks = await _unitOfWork.Tasks
                    .Query()
                    .Where(task => task.Project.OwnerId == UserId
                     && task.DueDate < DateTime.UtcNow
                     && task.Status != TaskStatusEnum.Done)
                    .Include(task => task.Assignee)
                    .Include(task => task.TaskTag)
                    .ThenInclude(tt => tt.Tag)
                    .OrderBy(task => task.DueDate)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
            }

            public async Task<Dictionary<string, int>> GetTaskSummaryAsync(int projectId)
            {
                var summary = await _unitOfWork.Tasks
                    .Query()
                    .Where(task => task.ProjectId == projectId)
                    .GroupBy(task => task.Status)
                    .Select(group => new
                    {
                        Status = group.Key.ToString(),
                        Count = group.Count()
                    }
                    )
                    .ToDictionaryAsync(task => task.Status, task => task.Count);

                return summary;
            }


            public async Task<TaskResponse?> GetTaskByIdAsync(int taskId)
            {
                var task = await _unitOfWork.Tasks
                    .Query()
                    .Include(task => task.Assignee)
                    .FirstOrDefaultAsync(task => task.Id ==  taskId);

            if (task == null)
                return null;

                return _mapper.Map<TaskResponse>(task);
            }

            public async Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request)
            {
                var task = _mapper.Map<AppTaskEntity>(request);

                await _unitOfWork.Tasks.AddAsync(task);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<TaskResponse>(task);
            }

            public async Task<TaskResponse> UpdateTaskAsync(int taskId,UpdateTaskRequest request)
            {
                var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);

            if (task == null)
                throw new TaskNotFoundException("Task not Available...");

                task.Title = request.Title;
                task.Description = request.Description;
                task.Priority = request.Priority;
                task.Status = request.Status;
                task.DueDate = request.DueDate;
                task.AssigneeId = request.AssigneeId;

                _unitOfWork.Tasks.Update(task);
                await _unitOfWork.SaveChangesAsync();

            var result = await GetTaskByIdAsync(taskId);

            if (result == null)
                throw new TaskNotFoundException("Unable to find the task...");

            return result;
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
        }
    }
