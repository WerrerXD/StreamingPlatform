using Microsoft.EntityFrameworkCore;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Repositories;

public class ReportRepository: Repository<Report>, IReportRepository
{
    public ReportRepository(ApplicationDbContext context)
        :base(context)
    {
    }

    public async Task<Report> GetReportByIdAsync(Guid reportId, CancellationToken cancellationToken)
    {
        var report = await _context.Reports
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == reportId, cancellationToken);
        
        return report;
    }
}