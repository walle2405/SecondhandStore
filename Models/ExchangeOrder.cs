using System;
using System.Collections.Generic;

#nullable disable

namespace SecondhandStore.Models
{
    public partial class ExchangeOrder
    {
        public int OrderDetailId { get; set; }
        public string ReceiverId { get; set; }
        public string AccountId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public int PostId { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverEmail { get; set; }

        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
        public virtual Account Receiver { get; set; }
    }
}
