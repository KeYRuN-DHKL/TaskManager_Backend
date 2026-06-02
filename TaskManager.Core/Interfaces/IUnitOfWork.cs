using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserEntity> Users { get; }
        IRepository<ProjectEntity> Projects { get; }
        IRepository<AppTaskEntity> Tasks { get; }
        IRepository<TagEntity> Tags { get; }

        Task<int> SaveChangesAsync();
    }
}
