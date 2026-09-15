using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.Project
{
    public record CreateProjectRequest
    (
        string Name,
        string? Description
    );
}
