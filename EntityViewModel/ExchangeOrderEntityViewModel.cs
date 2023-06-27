namespace SecondhandStore.EntityViewModel
{
    public class ExchangeOrderEntityViewModel
    {
        public int OrderDetailId { get; set; }
        public string ReceiverId { get; set; }
        public string AccountId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public int PostId { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
