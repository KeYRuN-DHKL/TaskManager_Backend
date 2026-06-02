using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.Auth;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) =>
            _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            
            if(result == null)
            {
                return BadRequest("Unable to Log you in...");
            }

            return Ok(result);
        }
    }
}
