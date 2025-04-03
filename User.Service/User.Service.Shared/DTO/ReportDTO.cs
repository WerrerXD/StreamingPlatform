namespace User.Service.Shared.DTO;

public record ReportDTO
{
    public Guid Id { get; init; }
    public Guid ReporterId { get; init; }
    public Guid ReportedId { get; init; }
    public string Reason { get; init; }
    public string Status { get; init; }
    public DateTime CreatedAt { get; init; }
}