using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public string Reason { get; set; } = null!;
        public string ReportedAccountId { get; set; } = null!;
        public DateTime ReportDate { get; set; }
        public string Evidence1 { get; set; } = null!;
        public string Evidence2 { get; set; } = null!;
        public string Evidence3 { get; set; } = null!;

        public virtual Account ReportedAccount { get; set; } = null!;
    }
}
