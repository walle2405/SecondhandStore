namespace SecondhandStore.EntityViewModel;

public class PostEntityViewModel
{

    public int PostId { get; set; }
    public string AccountId { get; set; }
    public string Fullname { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string ProductName { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string PostStatus { get; set; }
    public string PostType { get; set; } = null!;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int PointCost { get; set; }
    public DateTime PostDate { get; set; }
    public int PostPriority { get; set; }
    public DateTime PostExpiryDate { get; set; }
    public double Price { get; set; }

}

