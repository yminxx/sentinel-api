using Sentinel.Application.Authentication.DTOs;
using Sentinel.Application.Authentication.Responses;
using Sentinel.Application.Common;

namespace Sentinel.Application.Abstractions.Authentication;

public interface IAuthService
{
    Task<OperationResult<object>> RegisterAsync(RegisterRequest request);
    Task<OperationResult<AuthResponse>> LoginAsync(LoginRequest request);
    Task<OperationResult<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
