namespace SecondhandStore.EntityViewModel;

public class TopUpEntityViewModel
{
    public int OrderId { get; set; }
    public int TopUpPoint { get; set; }
    public int AccountId { get; set; }
    public DateTime TopUpDate { get; set; }
    public double Price { get; set; }
}