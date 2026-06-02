using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.Tag
{
    public record TagResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Color { get; init; }
    }
}
