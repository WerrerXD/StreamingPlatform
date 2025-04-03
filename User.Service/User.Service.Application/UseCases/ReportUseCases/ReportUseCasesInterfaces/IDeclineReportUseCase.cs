namespace User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;

public interface IDeclineReportUseCase
{
    Task ExecuteAsync(Guid reportId, CancellationToken cancellationToken);
}