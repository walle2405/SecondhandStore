using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class ExchangeOrder
    {
        public int OrderId { get; set; }
        public int PostId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }

        public virtual Account Buyer { get; set; }
        public virtual PostStatus OrderStatus { get; set; }
        public virtual Post Post { get; set; }
        public virtual Account Seller { get; set; }
    }
}
