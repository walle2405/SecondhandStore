namespace SecondhandStore.EntityRequest
{
    public class ReportCreateRequest
    {
        public string ReporterId { get; set; }
        public string ReportedAccountId { get; set; }
        public string Reason { get; set; }
        public string Evidence1 { get; set; } = null!;
        public string? Evidence2 { get; set; }
        public string? Evidence3 { get; set; }
    }
}
