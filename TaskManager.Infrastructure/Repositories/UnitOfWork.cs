using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<UserEntity> Users { get; }
        public IRepository<ProjectEntity> Projects { get; }
        public IRepository<AppTaskEntity> Tasks { get; }
        public IRepository<TagEntity> Tags { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new GenericRepository<UserEntity>(context);
            Projects = new GenericRepository<ProjectEntity>(context);
            Tasks = new GenericRepository<AppTaskEntity>(context);
            Tags = new GenericRepository<TagEntity>(context);
        }

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public void Dispose() =>
             _context.Dispose();
    }
}
