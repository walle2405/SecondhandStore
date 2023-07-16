using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public string Reason { get; set; } = null!;
        public int ReporterId { get; set; }
        public int ReportedAccountId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; } = null!;

        public virtual Account ReportedAccount { get; set; } = null!;
        public virtual Account Reporter { get; set; } = null!;
    }
}
