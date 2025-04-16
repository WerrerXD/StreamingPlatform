using Microsoft.AspNetCore.Http;
using User.Service.Application.Abstractions;
using User.Service.Application.Contracts;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class ChangeUserProfile : IChangeUserProfile
{
    private readonly IUserRepository _userRepository;
    private readonly IFileStorageService _fileStorageService;

    public ChangeUserProfile(IUserRepository userRepository, IFileStorageService fileStorageService)
    {
        _userRepository = userRepository;
        _fileStorageService = fileStorageService;
    }
    
    public async Task ExecuteAsync(Guid userId, ChangeUserProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException("User does not exist");
        
        if (request.AvatarPhoto != null)
        {
            var coverUrl = await _fileStorageService.SaveFileAsync(request.AvatarPhoto, "users-avatars");
            
            user.AvatarUrl = coverUrl;
        }

        if (!string.IsNullOrEmpty(request.Username))
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
            
            if (existingUser != null)
            {
                throw new AlreadyExistsException("User with this username already exists"); 
            }
            
            user.UserName = request.Username;
        }
        
        if (!string.IsNullOrEmpty(request.Description))
        {
            user.Description = request.Description;
        }
        
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}