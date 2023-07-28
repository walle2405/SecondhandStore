namespace SecondhandStore.EntityViewModel;

public class TopUpEntityViewModel
{
    public int orderId { get; set; }
    public int topUpPoint { get; set; }
    public int accountId { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public DateTime topUpDate { get; set; }
    public double price { get; set; }
    public string topUpStatus { get; set; }
}