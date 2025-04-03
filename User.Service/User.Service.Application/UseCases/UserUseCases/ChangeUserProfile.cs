using Microsoft.AspNetCore.Http;
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
        if (!await _userRepository.IsExistByIdAsync(userId, cancellationToken))
            throw new NotFoundException("User does not exist");
        

        if (avatarPhoto != null)
        {
            var coverUrl = await _fileStorageService.SaveFileAsync(avatarPhoto, "users-avatars");
            await _userRepository.SetAvatarUrl(userId, coverUrl, cancellationToken);
        }

        if (!string.IsNullOrEmpty(userName))
        {
            var testUser = await _userRepository.GetByUsernameAsync(userName, cancellationToken);
            if (testUser != null)
            {
                throw new AlreadyExistsException("User with this username already exists"); 
            }
            await _userRepository.SetUserName(userId, userName, cancellationToken);
        }
        
        if (!string.IsNullOrEmpty(description))
        {
            await _userRepository.SetDescription(userId, description, cancellationToken);
        }
        
        await _userRepository.Save(cancellationToken);
    }
}