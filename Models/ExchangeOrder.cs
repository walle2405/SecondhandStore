using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class ExchangeOrder
    {
        public ExchangeOrder()
        {
            Reviews = new HashSet<Review>();
        }

        public int OrderId { get; set; }
        public int PostId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }

        public virtual Account Buyer { get; set; } = null!;
        public virtual PostStatus OrderStatus { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual Account Seller { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
