using TaskManager.Core.Enum;

namespace TaskManager.Core.DTOs.AppTask
{
    public record CreateTaskRequest
    (
        string Title,
        string Description,
        TaskPriorityEnum Priority,
        DateTime DueDate,
        int ProjectId,
        int? AssigneeId,
        List<int> TagIds
        );
}
