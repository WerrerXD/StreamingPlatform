namespace User.Service.Shared.DTO;

public record LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}