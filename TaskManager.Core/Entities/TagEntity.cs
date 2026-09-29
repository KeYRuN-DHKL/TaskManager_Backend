using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class TagEntity : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<TaskTagEntity> TaskTag { get; set; } = new List<TaskTagEntity>();
    }
}
