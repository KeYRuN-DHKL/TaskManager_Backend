using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.DTOs.Tag
{
    public record CreateTagRequest
    {
        string Name;
        string color;
    }
}
