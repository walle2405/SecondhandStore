namespace SecondhandStore.EntityViewModel
{
    public class ExchangeViewEntityModel
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatusName { get; set; }
    }
}
