using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
        Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        void Add(User user);
    }
}
