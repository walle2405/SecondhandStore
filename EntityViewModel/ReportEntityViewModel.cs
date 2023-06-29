using System.Security.Policy;

namespace SecondhandStore.EntityViewModel
{
    public class ReportEntityViewModel
    {
        public int ReportId { get; set; }
        public string ReporterId { get; set; }
        public string ReportedAccountId { get; set; }
        public DateTime ReportDate { get; set;}
        public string Evidence1 { get; set; }
        public string Evidence2 { get; set;}
        public string Evidence3 { get; set;}
    }
}
