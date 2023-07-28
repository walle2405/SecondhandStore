namespace SecondhandStore.EntityRequest;

public class PostUpdateRequest
{
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; } = 0;
    public IFormFileCollection? ImageUploadRequest { get; set; }
}   