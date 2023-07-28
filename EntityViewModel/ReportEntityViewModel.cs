using System.Security.Policy;
using SecondhandStore.Models;

namespace SecondhandStore.EntityViewModel
{
    public class ReportEntityViewModel
    {
        public int reportId { get; set; }
        public string reporterName { get; set; }
        public string reporterEmail { get; set; }
        public string reportedUserEmail { get; set; }
        public string reportedUserName { get; set; }
        public string reason { get; set; }
        public virtual ICollection<ReportImage> reportImages { get; set; }
        public string status { get; set; }
        public DateTime reportDate { get; set;}
    }
}
