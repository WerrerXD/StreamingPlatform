namespace Stream.Service.DataAccess.Messaging;

public record UserResponseEvent(
    Guid RequestId,
    Guid UserId
    );