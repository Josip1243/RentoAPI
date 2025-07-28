﻿using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) => _context = context;

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _context.SaveChangesAsync(ct);
    }
}
