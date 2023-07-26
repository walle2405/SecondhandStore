namespace SecondhandStore.EntityViewModel
{
    public class ExchangeOrderEntityViewModel
    {
        public int orderId { get; set; }
        public int? postId { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
        public int buyerId { get; set; }
        public string buyerName { get; set; }
        public string buyerPhoneNumber { get; set; }
        public string buyerEmail { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatusName { get; set; }
    }
}
