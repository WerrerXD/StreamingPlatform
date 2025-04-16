using User.Service.Application.Contracts;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Enums;
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
    
    public async Task ExecuteAsync(Guid reporterId, ReportUserRequest request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsExistByIdAsync(reporterId, cancellationToken))
        {
            throw new NotFoundException("User is not found");
        }

        if (!await _userRepository.IsExistByIdAsync(request.ReportedId, cancellationToken))
        {
            throw new NotFoundException("User you are going to report is not found");
        }

        Report reportModel = new()
        {
            ReporterId = reporterId,
            ReportedId = request.ReportedId,
            Reason = request.Reason,
            Status = ReportStatus.Pending.ToString()
        };
        
        await _reportRepository.CreateAsync(reportModel, cancellationToken);
    }
}