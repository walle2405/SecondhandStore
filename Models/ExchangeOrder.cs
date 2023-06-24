#nullable disable

namespace SecondhandStore.Models;

public class ExchangeOrder
{
    public int OrderDetailId { get; set; }
    public int PostId { get; set; }
    public virtual Post Post { get; set; }
    public string SellerId { get; set; }
    public virtual Account Seller { get; set; }
    public string BuyerId { get; set; }
    public virtual Account Buyer { get; set; }
    public DateTime OrderDate { get; set; }
    public bool OrderStatus { get; set; }
    public string BuyerPhoneNumber { get; set; }
    public string BuyerEmail { get; set; }
}