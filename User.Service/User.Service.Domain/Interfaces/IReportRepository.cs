using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IReportRepository: IRepository<Report>
{
    Task<Report> GetReportByIdAsync(Guid reportId, CancellationToken cancellationToken);
}