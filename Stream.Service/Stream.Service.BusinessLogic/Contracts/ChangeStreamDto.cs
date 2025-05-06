namespace Stream.Service.BusinessLogic.Contracts;

public record ChangeStreamDto(
    string? StreamName,
    string? StreamDescription,
    string? CategoryId
    );