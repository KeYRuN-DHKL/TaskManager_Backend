using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.AppTask
{
    public record TaskResponse
    {
        //int Id,
        //string Title,
        //string Descripton,
        //string Status,
        //string Priority,
        //DateTime DueDate,
        //int ProjectId,
        //int AssigneeId

        public int Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }

        public required string Status { get; init; }
        public required string Priority { get; init; }
        public DateTime DueDate { get; init; }
        public int ProjectId { get; init; }
        public int AssigneeId { get; init; }
        public required List<string> Tags { get; init; } = new List<string>();
        public DateTime CreatedAt { get; init; }
    };
}
