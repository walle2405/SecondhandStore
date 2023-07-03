namespace SecondhandStore.EntityViewModel;

public class TopUpEntityViewModel
{
    public int OrderId { get; set; }
    public int TopUpPoint { get; set; }
    public int AccountId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime TopUpDate { get; set; }
    public double Price { get; set; }
    public string TopUpStatus { get; set; }
}