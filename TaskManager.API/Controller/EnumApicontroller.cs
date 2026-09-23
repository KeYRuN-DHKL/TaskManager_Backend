using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Enum;

namespace TaskManager.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnumApiController : ControllerBase
    {
        [HttpGet("enums/task-status")]
        [Authorize] 
        public IActionResult GetTaskStatuses()
        {
            var values = Enum.GetNames(typeof(TaskStatusEnum));
            return Ok(values);
        }

        [HttpGet("enums/task-priority")]
        public IActionResult GetTaskPriorities()
        {
            var values = Enum.GetNames(typeof(TaskPriorityEnum));
            return Ok(values);
        }
    }
}
