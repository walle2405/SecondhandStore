#nullable disable

namespace SecondhandStore.Models;

public class Account
{
    public Account()
    {
        ExchangeOrderAccounts = new HashSet<ExchangeOrder>();
        ExchangeOrderReceivers = new HashSet<ExchangeOrder>();
        ExchangeRequests = new HashSet<ExchangeRequest>();
        Posts = new HashSet<Post>();
        Reports = new HashSet<Report>();
        Reviews = new HashSet<Review>();
        TopUps = new HashSet<TopUp>();
    }

    public string AccountId { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string RoleId { get; set; }
    public virtual Role Role { get; set; }
    public string Address { get; set; }
    public string PhoneNo { get; set; }
    public bool IsActive { get; set; }
    public double UserRatingScore { get; set; }
    public int PointBalance { get; set; }

    public virtual ICollection<ExchangeOrder> ExchangeOrderAccounts { get; set; }
    public virtual ICollection<ExchangeOrder> ExchangeOrderReceivers { get; set; }
    public virtual ICollection<ExchangeRequest> ExchangeRequests { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<TopUp> TopUps { get; set; }
}