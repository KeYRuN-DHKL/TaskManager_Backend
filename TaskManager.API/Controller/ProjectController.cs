using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Core.DTOs.Project;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

     private int CurrentUserId
        {
            get
            {
                return Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetUserProjectsAsync(CurrentUserId);
            return Ok(projects);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProjectById([FromRoute] int id)
        {
            var project = await _projectService.GetProjectByIdAsync(CurrentUserId,id);

            if (project == null)
                return NotFound("Project Not Found");

            return Ok(project);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var project = await _projectService.CreateProjectAsync(request,CurrentUserId);
            return Ok(project);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request,[FromRoute] int id)
        {
            var project = await _projectService.UpdateProjectAsync(request, CurrentUserId,id);

             return project == null ? NotFound("Porject Not Found") :  Ok(project);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            var isDeleted = await _projectService.DeleteProjectAsync(id,CurrentUserId);

            return isDeleted ? Ok("Project Deleted Successfully...") : NotFound();
        }
    }
}
