namespace SecondhandStore.EntityViewModel
{
    public class ExchangeViewEntityModel
    {
        public int orderId { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
        public string buyerName { get; set; }
        public string buyerEmail { get; set; }
        public string sellerName { get; set; }
        public string sellerEmail { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatusName { get; set; }
    }
}
