namespace SecondhandStore.EntityRequest;

public class PostCreateRequest
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int PointCost { get; set; }
    public int PostStatusId = 2;
    public int CategoryId { get; set; }
    public bool isDonated { get; set; }
    public double Price { get; set; }
    public IFormFileCollection? ImageUploadRequest { get; set; }
}