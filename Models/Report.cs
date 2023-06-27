using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
<<<<<<< HEAD
        public string Reason { get; set; }
        public string ReporterId { get; set; }
        public string ReportedAccountId { get; set; }
=======
        public string Reason { get; set; } = null!;
        public string ReporterId { get; set; } = null!;
        public string ReportedAccountId { get; set; } = null!;
>>>>>>> 0328c1e978e95085f75972c075248f9a53d59742
        public DateTime ReportDate { get; set; }
        public string Evidence1 { get; set; } = null!;
        public string? Evidence2 { get; set; }
        public string? Evidence3 { get; set; }

<<<<<<< HEAD
        public virtual Account ReportedAccount { get; set; }
        public virtual Account Reporter { get; set; }
=======
        public virtual Account ReportedAccount { get; set; } = null!;
        public virtual Account Reporter { get; set; } = null!;
>>>>>>> 0328c1e978e95085f75972c075248f9a53d59742
    }
}
