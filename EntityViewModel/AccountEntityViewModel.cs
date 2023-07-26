using SecondhandStore.Models;

namespace SecondhandStore.EntityViewModel;

public class AccountEntityViewModel
{
    public int accountId { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public DateTime dob { get; set; }
    public string roleId { get; set; }
    public string address { get; set; }
    public string phoneNo { get; set; }
    public int pointBalance { get; set; }
    public int credibilityPoint { get; set; }
    public bool isActive { get; set; }
}