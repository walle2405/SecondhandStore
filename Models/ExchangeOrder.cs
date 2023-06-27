using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class ExchangeOrder
    {
        public int OrderId { get; set; }
<<<<<<< HEAD
        public int PostId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

        public virtual Account Buyer { get; set; }
        public virtual Post Post { get; set; }
        public virtual Account Seller { get; set; }
=======
        public int? PostId { get; set; }
        public string SellerId { get; set; } = null!;
        public string BuyerId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }

        public virtual Account Buyer { get; set; } = null!;
        public virtual Post? Post { get; set; }
        public virtual Account Seller { get; set; } = null!;
>>>>>>> 0328c1e978e95085f75972c075248f9a53d59742
    }
}
