using Microsoft.EntityFrameworkCore;
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

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context
            .RefreshTokens.Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
