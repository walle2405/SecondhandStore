namespace SecondhandStore.EntityViewModel
{
    public class ExchangeRequestEntityViewModel
    {
        public int orderId { get; set; }
        public int? postId { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
        public int sellerId { get; set; }
        public string sellerName { get; set; }
        public string sellerPhoneNumber { get; set; }
        public string sellerEmail { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatusName { get; set; }
    }
}
