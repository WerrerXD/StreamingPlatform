namespace User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;

public interface IReportUserUseCase
{
    Task ExecuteAsync(Guid reporterId, Guid reportedId, string reason, CancellationToken cancellationToken);
}