namespace User.Service.Application.Contracts;

public record UserLoggedInEvent(
    Guid UserId
    );