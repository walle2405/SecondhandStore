namespace SecondhandStore.EntityViewModel
{
    public class ReviewEntityViewModel
    {
        public int ReviewId { get; set; }
        public string ReviewerName { get; set;}
        public string ReviewerEmail { get; set; }
        public string ReviewedName { get; set; }
        public string ReviewedEmail { get; set;}
        public int RatingStar { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
