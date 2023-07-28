using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Status
    {
        public Status()
        {
            ExchangeOrders = new HashSet<ExchangeOrder>();
            Posts = new HashSet<Post>();
            Reports = new HashSet<Report>();
            TopUps = new HashSet<TopUp>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<TopUp> TopUps { get; set; }
    }
}
