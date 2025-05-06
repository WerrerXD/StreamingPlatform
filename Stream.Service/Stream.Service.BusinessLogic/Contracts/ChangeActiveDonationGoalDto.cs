namespace Stream.Service.BusinessLogic.Contracts;

public record ChangeActiveDonationGoalDto(
    string? Title,
    decimal? TargetAmount
    );