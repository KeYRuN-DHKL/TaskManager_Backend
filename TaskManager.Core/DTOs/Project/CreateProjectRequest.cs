namespace TaskManager.Core.DTOs.Project
{
    public record CreateProjectRequest
    (
        string Name,
        string? Description
    );
}
