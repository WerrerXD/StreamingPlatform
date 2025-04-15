namespace User.Service.Application.Contracts;

public record LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}