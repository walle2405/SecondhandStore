using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class ImageService
{
    private readonly ImageRepository _imageRepository;

    public ImageService(ImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task AddImage(List<string?> image, int postId)
    {
        await _imageRepository.Add(image, postId);
    }

    public async Task UpdateImage(Image image)
    {
        await _imageRepository.Update(image);
    }

    public async Task DeleteImage(Image image)
    {
        await _imageRepository.Delete(image);
    }
}