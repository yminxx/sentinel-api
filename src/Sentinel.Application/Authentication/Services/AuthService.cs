using Sentinel.Application.Abstractions.Authentication;
using Sentinel.Application.Abstractions.Persistence;
using Sentinel.Application.Authentication.DTOs;
using Sentinel.Application.Authentication.Responses;
using Sentinel.Domain.Entities;

namespace Sentinel.Application.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
        
    { 
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            throw new Exception("User Already Exists");
        }

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new Exception("Invalid Credentials");
        }
        
        var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
        
        if (!isPasswordValid)
        {
            throw new Exception("Invalid Credentials");
        }

        var token = _jwtProvider.GenerateToken(
            user.Id,
            user.Email,
            user.Role);

        return new AuthResponse
        {
            Token = token
        };
    }
}