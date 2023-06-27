﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SecondhandStore.Models
{
    public partial class ExchangeOrder
    {
        public int OrderId { get; set; }
        public int PostId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

        public virtual Account Buyer { get; set; }
        public virtual Post Post { get; set; }
        public virtual Account Seller { get; set; }
    }
}
