namespace SecondhandStore.EntityRequest
{
    public class ReviewCreateRequest
    {
        public int ReviewedId { get; set; }
        public int? RatingStar { get; set; }
        public string? Description { get; set; }
    }
}
