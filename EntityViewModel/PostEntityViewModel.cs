using SecondhandStore.Models;

namespace SecondhandStore.EntityViewModel;

public class PostEntityViewModel
{

    public int PostId { get; set; }
    public int AccountId { get; set; }
    public string Fullname { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string ProductName { get; set; }
    
    public virtual ICollection<Image> Images { get; set; }
    
    public string Description { get; set; }
    public string PostStatusId { get; set; }
    public string StatusName { get; set; }
    public bool IsDonated { get; set; }
    public int CategoryId { get; set; }
    public int CategoryValue { get; set; }
    public string CategoryName { get; set; }
    public double Price { get; set; }
    public DateTime CreatedDate { get; set; }
    

}

