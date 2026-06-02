namespace TaskManager.Core.DTOs.Auth
{
    public record AuthResponse
    (
        string Token,
        string UserName,
        string Email,
        string Role
        );
}
