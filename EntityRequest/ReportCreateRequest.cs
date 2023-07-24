namespace SecondhandStore.EntityRequest
{
    public class ReportCreateRequest
    {
        public string ReportedAccountId { get; set; }
        public string Reason { get; set; }
        public IFormFileCollection ImageUploadRequest { get; set; }
    }
}
