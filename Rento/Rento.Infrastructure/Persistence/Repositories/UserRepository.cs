using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default) =>
            await _context.Users.AnyAsync(u => u.Email == email, ct);

        public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
            await _context.Users.SingleOrDefaultAsync(u => u.Email == email, ct);

        public void Add(User user) => _context.Users.Add(user);
    }
}
