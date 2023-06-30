namespace SecondhandStore.EntityRequest
{
    public class ExchangeOrderCreateRequest
    {
        public string ReceiverId { get; set; }
        public int AccountId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public int PostId { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
