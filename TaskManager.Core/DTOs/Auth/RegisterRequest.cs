namespace TaskManager.Core.DTOs.Auth
{
    public record RegisterRequest(
        string Email,
        string Password,
        string UserName
        ); 
}
