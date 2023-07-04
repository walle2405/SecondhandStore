namespace SecondhandStore.EntityViewModel;

public class PostEntityViewModel
{

    public int PostId { get; set; }
    public string AccountId { get; set; }
    public string Fullname { get; set; }
    public string ProductName { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string PostTypeName { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int PointCost { get; set; }
    public DateTime PostDate { get; set; }
    public int PostPriority { get; set; }
    public DateTime PostExpiryDate { get; set; }
    public double Price { get; set; }
    public string PostStatusName { get; set; }

}

