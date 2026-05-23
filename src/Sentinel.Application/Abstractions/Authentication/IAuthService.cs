using Sentinel.Application.Authentication.DTOs;
using Sentinel.Application.Authentication.Responses;

namespace Sentinel.Application.Abstractions.Authentication;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);
    Task <AuthResponse> LoginAsync(LoginRequest request);
}