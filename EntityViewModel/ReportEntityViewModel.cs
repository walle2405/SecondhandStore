using System.Security.Policy;

namespace SecondhandStore.EntityViewModel
{
    public class ReportEntityViewModel
    {
        public int ReportId { get; set; }
        public string ReporterName { get; set; }
        public string ReporterEmail { get; set; }
        public string ReportedUserEmail { get; set; }
        public string ReportedUserName { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime ReportDate { get; set;}
    }
}
