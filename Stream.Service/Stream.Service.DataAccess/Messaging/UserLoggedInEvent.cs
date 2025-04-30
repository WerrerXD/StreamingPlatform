namespace Stream.Service.DataAccess.Messaging;

public record UserLoggedInEvent(
    Guid UserId
    );