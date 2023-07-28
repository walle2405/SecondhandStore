using SecondhandStore.Models;

namespace SecondhandStore.EntityViewModel;

public class PostEntityViewModel
{

    public int postId { get; set; }
    public int accountId { get; set; }
    public string fullname { get; set; }
    public string phoneNo { get; set; }
    public string address { get; set; }
    public string email { get; set; }
    public string productName { get; set; }
    
    public virtual ICollection<Image> images { get; set; }
    
    public string description { get; set; }
    public string postStatusId { get; set; }
    public string statusName { get; set; }
    public bool isDonated { get; set; }
    public int categoryId { get; set; }
    public int categoryValue { get; set; }
    public string categoryName { get; set; }
    public double price { get; set; }
    public DateTime createdDate { get; set; }
    

}

