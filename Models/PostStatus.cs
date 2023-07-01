using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class PostStatus
    {
        public PostStatus()
        {
            ExchangeOrders = new HashSet<ExchangeOrder>();
            Posts = new HashSet<Post>();
        }

        public int PostStatusId { get; set; }
        public string PostStatusName { get; set; }

        public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
