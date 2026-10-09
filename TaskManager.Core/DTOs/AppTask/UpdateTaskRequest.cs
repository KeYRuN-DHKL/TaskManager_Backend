using TaskManager.Core.Enum;

namespace TaskManager.Core.DTOs.AppTask
{
    public record UpdateTaskRequest
    (
        string Title,
        TaskStatusEnum Status,
        TaskPriorityEnum Priority,
        DateTime DueDate,
        int? AssigneeId
    );
}
