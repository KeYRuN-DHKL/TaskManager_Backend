using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.AppTask;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IAppTaskService _appTaskService;

        public TaskController(IAppTaskService appTaskService)
        {
            _appTaskService = appTaskService;
        }

        [HttpGet("projects/{projectId}")]
        public async Task<IActionResult> GetTaskByProject([FromRoute] int projectId)
        {
            var tasks = await _appTaskService.GetTasksByProjectAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("project")]
        public async Task<IActionResult> GetTaskById([FromQuery] int taskId)
        {
            var task = await _appTaskService.GetTaskByIdAsync(taskId);

            if (task == null)
                return NotFound("Task not found... ");

            return Ok(task);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            //var updatedRequest = request with { ProjectId = projectId };
            var task = await _appTaskService.CreateTaskAsync(request);
            return Ok(task);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateTask([FromQuery] int taskId,[FromBody] UpdateTaskRequest request)
        {
            var task = await _appTaskService.UpdateTaskAsync(taskId,request);

            if (task == null)
                return NotFound("Task not found...");

            return Ok(task);
        }

        [HttpDelete("{taskId:int}")]
        public async Task<IActionResult> DeleteTask([FromQuery] int taskId)
        {
            var IsDeleted = await _appTaskService.DeleteTaskAsync(taskId);

            if (!IsDeleted)
                return NotFound("Task not found...");

            return Ok("Task Deleted Successfully...");
        }
    }
}
