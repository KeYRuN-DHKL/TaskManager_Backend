using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.Tag;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
        {
            var createdTag = await _tagService.CreateTagAsync(request);
            return Ok(createdTag);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagByIdAsync([FromRoute] int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return tag == null ? NotFound("Tag Not Available...") : Ok(tag);
        }
    }
}
