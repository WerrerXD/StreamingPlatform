using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IReportRepository: IRepository<Report>
{
    Task ReportUser(Report report, CancellationToken cancellationToken);
    Task SetStatus(Guid reportId, string status, CancellationToken cancellationToken);
    Task<Report> GetReportById(Guid reportId, CancellationToken cancellationToken);
}