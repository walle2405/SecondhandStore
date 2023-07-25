using System.ComponentModel.DataAnnotations;

namespace SecondhandStore.EntityRequest
{
    public class ReportCreateRequest
    {
        public string ReportedAccountId { get; set; }
        public string Reason { get; set; }
        [Required(ErrorMessage = "Need to insert at least 1 image")]
        public IFormFileCollection ImageUploadRequest { get; set; }
    }
}
