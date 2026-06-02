using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int OwnerId { get; set; }
        public required UserEntity Owner { get; set; }
        public ICollection<AppTaskEntity> Tasks { get; set; } = new List<AppTaskEntity>();
    }
}
