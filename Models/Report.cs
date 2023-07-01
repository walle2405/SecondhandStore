using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public string Reason { get; set; }
        public int ReporterId { get; set; }
        public int ReportedAccountId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Evidence1 { get; set; }
        public string Evidence2 { get; set; }
        public string Evidence3 { get; set; }

        public virtual Account ReportedAccount { get; set; }
        public virtual Account Reporter { get; set; }
    }
}
