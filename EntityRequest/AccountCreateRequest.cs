using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SecondhandStore.EntityRequest;

public class AccountCreateRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string PhoneNo { get; set; }
    public DateTime Dob { get; set; }
    public int credibilityPoint = 50;
    public DateTime createdDate = DateTime.Now;
    public string RoleId = "US";
    public bool IsActive = true;
}