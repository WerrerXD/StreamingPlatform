using Elastic.Clients.Elasticsearch.Snapshot;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Enums;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.ReportUseCases;

public class ApproveReportAndBanUserUseCase : IApproveReportAndBanUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IReportRepository _reportRepository;

    public ApproveReportAndBanUserUseCase(IUserRepository userRepository, IReportRepository reportRepository)
    {
        _userRepository = userRepository;
        _reportRepository = reportRepository;
    }
    
    public async Task ExecuteAsync(Guid reportId, int daysBanned, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetReportByIdAsync(reportId, cancellationToken) 
            ?? throw new NotFoundException("Report is not found");

        var isReporterExist = await _userRepository.IsExistByIdAsync(report.ReporterId, cancellationToken);

        if (!isReporterExist)
        {
            throw new NotFoundException("User that is reported does not exist");
        }
        
        var reportedUser = await _userRepository.GetByIdAsync(report.ReportedId, cancellationToken)
            ?? throw new NotFoundException("User you are going to block is not found");
        
        reportedUser.IsBlocked = true;
        reportedUser.BlockedUntil = DateTime.UtcNow.AddDays(daysBanned);
        
        await _userRepository.UpdateAsync(reportedUser, cancellationToken);
        
        report.Status = ReportStatus.Approved.ToString();
        
        await _reportRepository.UpdateAsync(report, cancellationToken);
    }
}