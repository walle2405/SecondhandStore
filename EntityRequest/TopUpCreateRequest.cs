namespace SecondhandStore.EntityRequest
{
    public class TopUpCreateRequest
    {
        public int OrderId { get; set; }
        public int TopUpPoint { get; set; }
        public string AccountId { get; set; }
        public DateTime TopUpDate { get; set; }
        public double Price { get; set; }
    }
}
