using TaskManager.Core.Enum;

namespace TaskManager.Core.DTOs.AppTask
{
    public record TaskFilterParams
    {
        public TaskStatusEnum? Status { get; init; }
        public TaskPriorityEnum? Priority { get; init; }
        public string? Tag { get; init; }
        public bool? IsOverdue { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
