﻿using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Account
    {
        public Account()
        {
            ExchangeOrderBuyers = new HashSet<ExchangeOrder>();
            ExchangeOrderSellers = new HashSet<ExchangeOrder>();
            Posts = new HashSet<Post>();
            ReportReportedAccounts = new HashSet<Report>();
            ReportReporters = new HashSet<Report>();
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

        public virtual Role Role { get; set; }
        public virtual ICollection<ExchangeOrder> ExchangeOrderBuyers { get; set; }
        public virtual ICollection<ExchangeOrder> ExchangeOrderSellers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> ReportReportedAccounts { get; set; }
        public virtual ICollection<Report> ReportReporters { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TopUp> TopUps { get; set; }
    }
}
