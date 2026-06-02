using TaskManager.Core.Enum;

namespace TaskManager.Core.Entities
{
    public class AppTaskEntity : BaseEntity
    {
        public required string Title { get; set; } 
        public string? Description { get; set; }
        public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Todo;
        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
        public int? AssigneeId { get; set; }

        public required ProjectEntity Project { get; set; }
        public UserEntity? Assignee { get; set; }

        public ICollection<TaskTagEntity> TaskTag = new List<TaskTagEntity>();
    }
}
