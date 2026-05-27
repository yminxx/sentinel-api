using Sentinel.Domain.Entities;

namespace Sentinel.Application.Abstractions.Persistence;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task SaveChangesAsync();
}
