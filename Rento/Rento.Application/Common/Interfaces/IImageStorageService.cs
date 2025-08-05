using Microsoft.AspNetCore.Http;

namespace Rento.Application.Common.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default);

    }
}
