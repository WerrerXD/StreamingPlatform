using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
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
        var report = await _reportRepository.GetReportById(reportId, cancellationToken) ?? 
                     throw new NotFoundException("Report is not found");
        if(!await _userRepository.IsExistByIdAsync(report.ReporterId, cancellationToken))
            throw new NotFoundException("User that is reported does not exist");
        if(!await _userRepository.IsExistByIdAsync(report.ReportedId, cancellationToken))
            throw new NotFoundException("User you are going to block is not found");
        
        await _userRepository.BanUserUntil(report.ReportedId, daysBanned, cancellationToken);
        
        await _reportRepository.SetStatus(reportId,"Approved",cancellationToken);
        
        await _userRepository.Save(cancellationToken);
        await _reportRepository.Save(cancellationToken);
    }
}