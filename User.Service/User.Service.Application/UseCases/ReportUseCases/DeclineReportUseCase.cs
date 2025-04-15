using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Enums;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.ReportUseCases;

public class DeclineReportUseCase : IDeclineReportUseCase
{
    private readonly IReportRepository _reportRepository;

    public DeclineReportUseCase(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    
    public async Task ExecuteAsync(Guid reportId, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetReportByIdAsync(reportId, cancellationToken) 
            ?? throw new NotFoundException("Report is not found");
        
        report.Status = ReportStatus.Declined.ToString();
        
        await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}