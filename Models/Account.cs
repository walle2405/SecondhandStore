using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Account
    {
        public Account()
        {
            ExchangeOrders = new HashSet<ExchangeOrder>();
            Posts = new HashSet<Post>();
            Reports = new HashSet<Report>();
            Reviews = new HashSet<Review>();
            TopUps = new HashSet<TopUp>();
        }

        public string AccountId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public bool IsActive { get; set; }
        public double UserRatingScore { get; set; }
        public int PointBalance { get; set; }

        public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TopUp> TopUps { get; set; }
    }
}
