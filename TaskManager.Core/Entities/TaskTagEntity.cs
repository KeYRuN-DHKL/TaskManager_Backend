using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Entities
{
    public class TaskTagEntity : BaseEntity
    {
        public int TaskId { get; set; }
        public int TagId { get; set; }

        public required AppTaskEntity Task { get; set; }
        public required TagEntity Tag { get; set; } 
    }
}
