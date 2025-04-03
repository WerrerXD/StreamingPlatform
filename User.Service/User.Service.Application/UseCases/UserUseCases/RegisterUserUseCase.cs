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
    
    public async Task ExecuteAsync(string userName, string email, string password, CancellationToken cancellationToken)
    {
        var testUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (testUser != null)
        {
            throw new AlreadyExistsException("User with this email already exists"); 
        }
        var testUserName = await _userRepository.GetByUsernameAsync(userName, cancellationToken);
        if (testUserName != null)
        {
            throw new AlreadyExistsException("User with this username already exists"); 
        }
        var hashedPassword = _passwordHasher.Generate(password);

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = email,
            PasswordHash = hashedPassword
        };

        await _userRepository.Create(user, cancellationToken);
        await _userRepository.Save(cancellationToken);
    }
}