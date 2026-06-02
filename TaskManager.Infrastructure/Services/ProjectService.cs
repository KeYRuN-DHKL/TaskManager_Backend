using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.DTOs.Project;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectResponse>> GetUserProjectsAsync(int userId)
        {
            var projects = await _unitOfWork.Projects
                .Query()
                .Where(project => project.OwnerId == userId)
                .Include(project => project.Tasks)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProjectResponse>>(projects);
        }

        public async Task<ProjectResponse?> GetProjectByIdAsync(int userId,int projectId)
        {
            var project = await _unitOfWork.Projects
                .Query()
                .Where(project => project.OwnerId == userId && project.Id == projectId)
                .Include(project => project.Tasks)
                .FirstOrDefaultAsync();

            return _mapper.Map<ProjectResponse>(project);
        }

        public async Task<ProjectResponse> CreateProjectAsync(CreateProjectRequest request,int userId)
        {
            var project = _mapper.Map<ProjectEntity>(request);
            project.OwnerId = userId;

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProjectResponse>(project);
        } 

        public async Task<ProjectResponse?> UpdateProjectAsync(UpdateProjectRequest request,int userId,int projectId)
        {
            var project = await _unitOfWork.Projects
                .Query()
                .FirstOrDefaultAsync(project => project.Id == projectId && project.OwnerId == userId);

            if (project == null)
                return null;

            project.Name = request.Name;
            project.Description = request.Description;
            project.IsCompleted = request.IsCompleted;

            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProjectResponse>(project);
        }

        public async Task<bool> DeleteProjectAsync(int projectId,int userId)
        {
            var project = await _unitOfWork.Projects
                .Query()
                .FirstOrDefaultAsync(project => project.Id == projectId && project.OwnerId == userId);

            if (project == null)
                return false;

            project.IsDeleted = true;

            _unitOfWork.Projects.Update(project);
            int rowsAffected = await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
