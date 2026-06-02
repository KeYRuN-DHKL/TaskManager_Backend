using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.Project
{
    public record ProjectResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public bool IsCompleted { get; init; }
        public int TaskCount { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
