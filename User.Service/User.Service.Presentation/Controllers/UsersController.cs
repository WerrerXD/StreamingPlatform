using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using User.Service.Application.Contracts;
using User.Service.Application.UseCases.UserUseCases;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Interfaces;
using User.Service.Presentation.Extensions;
using User.Service.Shared;

namespace User.Service.Presentation.Controllers;

[ApiController]
[Route("user-service/users")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    
    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IRegisterUserUseCase _registerUserUseCase;
    private readonly ILoginUserUseCase _loginUserUseCase;
    private readonly IAddAdminUseCase _addAdminUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;
    private readonly IFollowUserUseCase _followUserUseCase;
    private readonly IUnfollowUserUseCase _unfollowUserUseCase;
    private readonly ILogOutUseCase _logOutUseCase;
    
    private readonly IBlockUserUseCase _blockUserUseCase;
    private readonly IChangeUserProfile _changeUserProfile;

    public UsersController(IGetAllUsersUseCase getAllUsersUseCase, IRegisterUserUseCase registerUserUseCase, ILoginUserUseCase loginUserUseCase, IAddAdminUseCase addAdminUseCase, IRefreshTokenUseCase refreshTokenUseCase, IFollowUserUseCase followUserUseCase, IUnfollowUserUseCase unfollowUserUseCase, IMapper mapper, IBlockUserUseCase blockUserUseCase, IChangeUserProfile changeUserProfile, ILogOutUseCase logOutUseCase, IOptions<JwtOptions> jwtOptions, IBlacklistedTokenRepository blacklistedTokenRepository)
    {
        _getAllUsersUseCase = getAllUsersUseCase;
        _registerUserUseCase = registerUserUseCase; 
        _loginUserUseCase = loginUserUseCase;
        _addAdminUseCase = addAdminUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
        _followUserUseCase = followUserUseCase;
        _unfollowUserUseCase = unfollowUserUseCase;
        _mapper = mapper;
        _blockUserUseCase = blockUserUseCase;
        _changeUserProfile = changeUserProfile;
        _logOutUseCase = logOutUseCase;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _getAllUsersUseCase.ExecuteAsync(cancellationToken);
        
        var response = _mapper.Map<List<UserDto>>(users);
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await _registerUserUseCase.ExecuteAsync(request, cancellationToken);

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{userId:guid}/roles/admin")]
    public async Task<IActionResult> AddAdminRoleToUser([FromRoute]Guid userId, CancellationToken cancellationToken)
    {
        await _addAdminUseCase.ExecuteAsync(userId, cancellationToken);
        
        return Ok(userId);
    }
    
    [HttpPost("tokens")]
    public async Task<IActionResult> Login([FromBody]LoginUserRequest request, CancellationToken cancellationToken)
    {
        var (accessToken, refreshToken) = await _loginUserUseCase.ExecuteAsync(request.Email, request.Password, cancellationToken);
        
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }
    
    [HttpPut("tokens")]
    public async Task<IActionResult> RefreshToken([FromQuery]string refreshToken, CancellationToken cancellationToken)
    {
        var (newAccessToken, newRefreshToken) = await _refreshTokenUseCase.ExecuteAsync(refreshToken, cancellationToken);
        
        return Ok(new { AccessToken =  newAccessToken, RefreshToken = newRefreshToken });
    }
    
    [Authorize]
    [HttpDelete("tokens")]
    public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();
        var accessToken = HttpContext.GetAccessToken();
        
        await _logOutUseCase.ExecuteAsync(accessToken, userId, cancellationToken);
        
        return Ok();
    }

    [Authorize]
    [HttpPost("{followeeId:guid}/followers")]
    public async Task<IActionResult> FollowUser([FromRoute]Guid followeeId, CancellationToken cancellationToken)
    {
        var followerId = HttpContext.GetUserId();
        
        await _followUserUseCase.ExecuteAsync(followerId, followeeId, cancellationToken);
        
        return Ok();
    }
    
    [Authorize]
    [HttpDelete("{followeeId:guid}/followers")]
    public async Task<IActionResult> UnfollowUser([FromRoute]Guid followeeId, CancellationToken cancellationToken)
    {
        var followerId = HttpContext.GetUserId();
        
        await _unfollowUserUseCase.ExecuteAsync(followerId, followeeId, cancellationToken);
        
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{userId:guid}/blocks")]
    public async Task<IActionResult> BlockUser([FromRoute]Guid userId, [FromQuery]int daysBanned, CancellationToken cancellationToken)
    {
        await _blockUserUseCase.ExecuteAsync(userId, daysBanned, cancellationToken);
        
        return Ok();
    }
    
    [Authorize]
    [HttpPatch("me/profile")]
    public async Task<IActionResult> ChangeUserProfile([FromForm]ChangeUserProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = HttpContext.GetUserId();
        
        await _changeUserProfile.ExecuteAsync(userId, request.Username, request.Description, request.AvatarPhoto, cancellationToken);
        
        return Ok();
    }
}