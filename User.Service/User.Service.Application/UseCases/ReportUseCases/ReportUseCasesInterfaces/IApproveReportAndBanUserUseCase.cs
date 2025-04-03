namespace User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;

public interface IApproveReportAndBanUserUseCase
{
    Task ExecuteAsync(Guid reportId, int daysBanned, CancellationToken cancellationToken);
}