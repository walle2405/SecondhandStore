namespace SecondhandStore.EntityViewModel
{
    public class ReviewEntityViewModel
    {
        public int reviewId { get; set; }
        public string reviewerName { get; set;}
        public string reviewerEmail { get; set; }
        public string reviewedName { get; set; }
        public string reviewedEmail { get; set;}
        public int ratingStar { get; set; }
        public string description { get; set; }
        public DateTime createdDate { get; set; }
    }
}
