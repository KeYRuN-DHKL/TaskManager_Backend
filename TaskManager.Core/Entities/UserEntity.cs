namespace TaskManager.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        // Navigation
        public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
        public ICollection<AppTaskEntity> AssignedTasks { get; set; } = new List<AppTaskEntity>();
    }
}
