using User.Service.Application.Contracts;

namespace User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;

public interface IReportUserUseCase
{
    Task ExecuteAsync(ReportUserRequest request, CancellationToken cancellationToken);
}