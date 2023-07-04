namespace SecondhandStore.Extension;

public class ImageExtension
{
    public static string? ImageExtensionChecker(string? fileName)
    {
        return Path.GetExtension(fileName)?.Replace(".", string.Empty);
    }
}