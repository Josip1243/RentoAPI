using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class VehicleImageRepository : IVehicleImageRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleImage?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.VehicleImages
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public void Remove(VehicleImage image)
        {
            _context.VehicleImages.Remove(image);
        }

        public async Task FixOrderForVehicleAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            var images = await _context.VehicleImages
                .Where(i => i.VehicleId == vehicleId)
                .OrderBy(i => i.Order)
                .ToListAsync(cancellationToken);

            for (int i = 0; i < images.Count; i++)
            {
                images[i].Order = i;
            }
        }
    }
}
