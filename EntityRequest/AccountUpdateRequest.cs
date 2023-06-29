using System.Text.Json.Serialization;
using SecondhandStore.Models;

namespace SecondhandStore.EntityRequest;

public class AccountUpdateRequest
{
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string PhoneNo { get; set; }
    
    // public string Email { get; private set; }
    // public AccountUpdateRequest()
    // {
    //     
    // }
    // public AccountUpdateRequest(Account existingAccount)
    // {
    //     Email = existingAccount.Email;
    // }
}

