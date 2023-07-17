using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class TopUp
    {
        public int OrderId { get; set; }
        public int TopUpPoint { get; set; }
        public int AccountId { get; set; }
        public DateTime TopUpDate { get; set; }
        public double Price { get; set; }
        public int TopupStatusId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Status TopupStatus { get; set; } = null!;
    }
}
