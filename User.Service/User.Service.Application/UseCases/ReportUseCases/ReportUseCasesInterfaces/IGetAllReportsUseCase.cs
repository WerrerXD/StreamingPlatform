using User.Service.Domain.Entities;

namespace User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;

public interface IGetAllReportsUseCase
{
    Task<List<Report>> ExecuteAsync(CancellationToken cancellationToken);
}