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
    
    public async Task ReportUser(Report report, CancellationToken cancellationToken)
    {
        await _context.Reports.AddAsync(report, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SetStatus(Guid reportId,string status, CancellationToken cancellationToken)
    {
        var report = await _context.Reports
            .FirstOrDefaultAsync(r => r.Id == reportId, cancellationToken);
        report.Status = status;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Report> GetReportById(Guid reportId, CancellationToken cancellationToken)
    {
        var report = await _context.Reports
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == reportId, cancellationToken);
        return report;
    }
}