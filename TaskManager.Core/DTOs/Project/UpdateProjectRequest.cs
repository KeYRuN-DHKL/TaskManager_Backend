using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.Project
{
    public record UpdateProjectRequest
    (
        string Name,
        string? Description,
        bool IsCompleted
    );
}
