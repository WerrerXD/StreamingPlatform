using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.Application.Contracts;
using User.Service.Application.UseCases.ReportUseCases.ReportUseCasesInterfaces;
using User.Service.Presentation.Extensions;

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
    public async Task<IActionResult> ReportUser([FromRoute]Guid reportedId, [FromQuery]string reason, CancellationToken cancellationToken)
    {
        var reporterId = HttpContext.GetUserId();

        var reportUserDto = new ReportUserRequest
        {
            ReporterId = reporterId,
            ReportedId = reportedId,
            Reason = reason
        };
        
        await _reportUserUseCase.ExecuteAsync(reportUserDto, cancellationToken);
        
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllReports(CancellationToken cancellationToken)
    {
        var reports = await _getAllReportsUseCase.ExecuteAsync(cancellationToken);
        
        var response = _mapper.Map<List<ReportDto>>(reports);
        
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{reportId:guid}/approval")]
    public async Task<IActionResult> ApproveReportAndBlockUser([FromRoute]Guid reportId, [FromQuery]int daysBanned, CancellationToken cancellationToken)
    {
        await _approveReportAndBanUserUseCase.ExecuteAsync(reportId, daysBanned, cancellationToken);
        
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{reportId:guid}/rejection")]
    public async Task<IActionResult> RejectReport([FromRoute]Guid reportId, CancellationToken cancellationToken)
    {
        await _declineReportUseCase.ExecuteAsync(reportId, cancellationToken);
        
        return Ok();
    }
}