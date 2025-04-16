using User.Service.Application.Abstractions;
using User.Service.Application.Contracts;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        if (existingUser != null)
        {
            throw new AlreadyExistsException("User with this email already exists"); 
        }
        
        var existingUserWithUsername = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        
        if (existingUserWithUsername != null)
        {
            throw new AlreadyExistsException("User with this username already exists"); 
        }
        
        var hashedPassword = _passwordHasher.Generate(request.Password);

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword
        };

        await _userRepository.CreateAsync(user, cancellationToken);
    }
}