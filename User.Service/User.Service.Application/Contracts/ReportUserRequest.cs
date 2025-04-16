namespace User.Service.Application.Contracts;

public record ReportUserRequest
{
    public Guid ReportedId { get; init; }
    public string Reason { get; init; }
}