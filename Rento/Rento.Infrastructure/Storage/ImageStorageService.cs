using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Rento.Application.Common.Interfaces;

public class ImageStorageService : IImageStorageService
{
    private readonly IWebHostEnvironment _env;

    public ImageStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "vehicles");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        // Vraćamo relativnu putanju koju frontend može koristiti
        return $"/images/vehicles/{fileName}";
    }

    public async Task DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default)
    {
        var fileName = Path.GetFileName(imageUrl);
        var fullPath = Path.Combine(_env.WebRootPath!, "images", "vehicles", fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        await Task.CompletedTask;
    }
}
