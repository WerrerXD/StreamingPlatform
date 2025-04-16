using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.ReportUseCases;

public class GetAllReportsUseCase : IGetAllReportsUseCase
{
    private readonly IReportRepository _reportRepository;

    public GetAllReportsUseCase(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    
    public async Task<List<Report>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await _reportRepository.GetAllAsync(cancellationToken);
    }
}