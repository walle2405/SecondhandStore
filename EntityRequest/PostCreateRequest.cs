namespace SecondhandStore.EntityRequest;

public class PostCreateRequest
{
    public string AccountId { get; set; }
    public string ProductName { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public bool PostStatus { get; set; }
    public int CategoryId { get; set; }
    public string PostType { get; set; }
    public int PointCost { get; set; }
    public DateTime PostDate { get; set; }
    public int PostPriority { get; set; }
    public DateTime PostExpiryDate { get; set; }
    public double Price { get; set; }
}