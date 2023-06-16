#nullable disable

namespace SecondhandStore.Models;

public class TopUp
{
    public int OrderId { get; set; }
    public int TopUpPoint { get; set; }
    public string AccountId { get; set; }
    public DateTime TopUpDate { get; set; }
    public double Price { get; set; }

    public virtual Account Account { get; set; }
}