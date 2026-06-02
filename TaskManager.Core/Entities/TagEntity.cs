using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class TagEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Color { get; set; } = "#6366f1";

        public ICollection<TaskTagEntity> TaskTag { get; set; } = new List<TaskTagEntity>();
    }
}
