namespace Stream.Service.BusinessLogic.Contracts;

public record DonateStreamerDto(
    string DonorId,
    string DonorName,
    decimal Amount,
    string Message
    );