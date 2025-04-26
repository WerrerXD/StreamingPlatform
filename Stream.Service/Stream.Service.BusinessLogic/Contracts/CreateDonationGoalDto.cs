namespace Stream.Service.BusinessLogic.Contracts;

public record CreateDonationGoalDto(
    string Title,
    decimal TargetAmount
    );