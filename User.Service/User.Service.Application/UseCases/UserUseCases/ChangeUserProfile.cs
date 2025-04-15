using Microsoft.AspNetCore.Http;
using User.Service.Application.Abstractions;
using User.Service.Application.Exceptions;
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
    
    public async Task ExecuteAsync(Guid userId, string? userName, string? description, IFormFile? avatarPhoto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException("User does not exist");
        
        if (avatarPhoto != null)
        {
            var coverUrl = await _fileStorageService.SaveFileAsync(avatarPhoto, "users-avatars");
            
            user.AvatarUrl = coverUrl;
        }

        if (!string.IsNullOrEmpty(userName))
        {
            var existingUser = await _userRepository.GetByUsernameAsync(userName, cancellationToken);
            
            if (existingUser != null)
            {
                throw new AlreadyExistsException("User with this username already exists"); 
            }
            
            user.UserName = userName;
        }
        
        if (!string.IsNullOrEmpty(description))
        {
            user.Description = description;
        }
        
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}