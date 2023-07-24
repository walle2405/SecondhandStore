using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Report
    {
        public Report()
        {
            ReportImages = new HashSet<ReportImage>();
        }

        public int ReportId { get; set; }
        public string Reason { get; set; } = null!;
        public int ReporterId { get; set; }
        public int ReportedAccountId { get; set; }
        public DateTime ReportDate { get; set; }
        public int ReportStatusId { get; set; }

        public virtual Status ReportStatus { get; set; } = null!;
        public virtual Account ReportedAccount { get; set; } = null!;
        public virtual Account Reporter { get; set; } = null!;
        public virtual ICollection<ReportImage> ReportImages { get; set; }
    }
}
