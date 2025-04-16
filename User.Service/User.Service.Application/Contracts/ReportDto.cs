namespace User.Service.Application.Contracts;

public record ReportDto
{
    public Guid Id { get; init; }
    public Guid ReporterId { get; init; }
    public Guid ReportedId { get; init; }
    public string Reason { get; init; }
    public string Status { get; init; }
    public DateTime CreatedAt { get; init; }
}