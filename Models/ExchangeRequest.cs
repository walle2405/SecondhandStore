using System;
using System.Collections.Generic;

#nullable disable

namespace SecondhandStore.Models
{
    public partial class ExchangeRequest
    {
        public int RequestDetailId { get; set; }
        public string SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int PostId { get; set; }
        public string SellerPhoneNumber { get; set; }
        public string SellerEmail { get; set; }

        public virtual Post Post { get; set; }
        public virtual Account Seller { get; set; }
    }
}
