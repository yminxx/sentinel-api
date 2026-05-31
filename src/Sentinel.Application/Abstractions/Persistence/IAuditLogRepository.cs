using Sentinel.Domain.Entities;

namespace Sentinel.Application.Abstractions.Persistence;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog auditLog);
    Task SaveChangesAsync();
    Task<List<AuditLog>> GetAllAsync();
}
