using Sentinel.Application.Abstractions.Authentication;
using Sentinel.Application.Abstractions.Persistence;
using Sentinel.Application.Authentication.DTOs;
using Sentinel.Application.Authentication.Responses;
using Sentinel.Domain.Entities;
using Sentinel.Application.Common;

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

    public async Task<OperationResult<object>> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return new OperationResult<object>
            {
                Success = false,
                Message = "User Already Exists"
            };
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
        
        return new OperationResult<object>
        {
            Success = true,
            Message = "User Registered Successfully"
        };
    }

    public async Task<OperationResult<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email); 
        
        if (user is null)
        {
            return new OperationResult<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }
        
        var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash); 
        
        if (!isPasswordValid)
        {
            return new OperationResult<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var token = _jwtProvider.GenerateToken(
            user.Id,
            user.Email,
            user.Role);

        return new OperationResult<AuthResponse>
        {
            Success = true,
            Message = "Login successful.",
            Data = new AuthResponse
            {
                Token = token
            }
        };
    }
}