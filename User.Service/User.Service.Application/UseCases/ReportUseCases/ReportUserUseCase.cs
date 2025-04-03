using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.ReportUseCases;

public class ReportUserUseCase : IReportUserUseCase
{
    private readonly IReportRepository _reportRepository;
    private readonly IUserRepository _userRepository;

    public ReportUserUseCase(IReportRepository reportRepository, IUserRepository userRepository)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
    }
    
    public async Task ExecuteAsync(Guid reporterId, Guid reportedId, string reason, CancellationToken cancellationToken)
    {
        if(!await _userRepository.IsExistByIdAsync(reporterId, cancellationToken))
            throw new NotFoundException("User is not found");
        if(!await _userRepository.IsExistByIdAsync(reportedId, cancellationToken))
            throw new NotFoundException("User you are going to report is not found");
        Report reportModel = new()
        {
            ReporterId = reporterId,
            ReportedId = reportedId,
            Reason = reason,
            Status = "Pending"
        };
        await _reportRepository.ReportUser(reportModel, cancellationToken);
    }
}