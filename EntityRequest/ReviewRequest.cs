namespace SecondhandStore.EntityRequest
{
    public class ReviewRequest
    {
        public int ReviewId { get; set; }
        public int PostId { get; set;}
        public string Content { get; set;}
        public int StarRating { get; set; }
        public string FeedbackUserId { get; set; }
        public string FeedbackUsername { get; set; }

    }
}
