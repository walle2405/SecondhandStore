namespace SecondhandStore.EntityRequest
{
    public class ReviewCreateRequest
    {
        public int PostId { get; set;}
        public string Content { get; set;}
        public int StarRating { get; set; }
        public string FeedbackUserId { get; set; }
        public string FeedbackUsername { get; set; }

    }
}
