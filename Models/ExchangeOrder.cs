using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class ExchangeOrder
    {
        public int OrderId { get; set; }
        public int? PostId { get; set; }
        public string SellerId { get; set; } = null!;
        public string BuyerId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public string BuyerPhoneNumber { get; set; } = null!;
        public string BuyerEmail { get; set; } = null!;

        public virtual Account Buyer { get; set; } = null!;
        public virtual Post? Post { get; set; }
    }
}
