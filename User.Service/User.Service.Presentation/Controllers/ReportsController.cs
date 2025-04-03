using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Shared.DTO;

namespace User.Service.Presentation.Controllers;

[ApiController]
[Route("user-service/reports")]
public class ReportsController: ControllerBase
{
    private readonly IMapper _mapper;
    
    private readonly IReportUserUseCase _reportUserUseCase;
    private readonly IGetAllReportsUseCase _getAllReportsUseCase;
    private readonly IApproveReportAndBanUserUseCase _approveReportAndBanUserUseCase;
    private readonly IDeclineReportUseCase _declineReportUseCase;

    public ReportsController(IReportUserUseCase reportUserUseCase, IGetAllReportsUseCase getAllReportsUseCase, IApproveReportAndBanUserUseCase approveReportAndBanUserUseCase, IDeclineReportUseCase declineReportUseCase, IMapper mapper)
    {
        _reportUserUseCase = reportUserUseCase;
        _getAllReportsUseCase = getAllReportsUseCase;
        _approveReportAndBanUserUseCase = approveReportAndBanUserUseCase;
        _declineReportUseCase = declineReportUseCase;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPost("{reportedId:guid}")]
    public async Task<IActionResult> ReportUser(Guid reportedId, string reason, CancellationToken cancellationToken)
    {
        var reporterId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _reportUserUseCase.ExecuteAsync(reporterId, reportedId,reason, cancellationToken);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllReports(CancellationToken cancellationToken)
    {
        var reports = await _getAllReportsUseCase.ExecuteAsync(cancellationToken);
        var response = reports.Select(r => _mapper.Map<ReportDTO>(r));
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{reportId:guid}/approval")]
    public async Task<IActionResult> ApproveReportAndBlockUser(Guid reportId, int daysBanned,CancellationToken cancellationToken)
    {
        await _approveReportAndBanUserUseCase.ExecuteAsync(reportId, daysBanned, cancellationToken);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{reportId:guid}/rejection")]
    public async Task<IActionResult> RejectReport(Guid reportId,CancellationToken cancellationToken)
    {
        await _declineReportUseCase.ExecuteAsync(reportId, cancellationToken);
        return Ok();
    }
}