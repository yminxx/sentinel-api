using Sentinel.Application.Abstractions.Persistence;
using Sentinel.Domain.Entities;

namespace Sentinel.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;
    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}