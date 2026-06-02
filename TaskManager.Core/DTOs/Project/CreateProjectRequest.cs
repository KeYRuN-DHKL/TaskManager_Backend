using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.Project
{
    public record CreateProjectRequest
    (
        int id,
        string Name,
        string? Description,
        DateTime createdAt
    );
}
